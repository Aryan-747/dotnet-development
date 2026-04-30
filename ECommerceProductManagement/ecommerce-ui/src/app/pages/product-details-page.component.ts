import { CommonModule } from '@angular/common';
import { Component, signal, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CatalogService } from '../services/catalog.service';
import { ApiService } from '../services/api.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { CartService } from '../services/cart.service';
import { Product, WorkflowSnapshot } from '../models';
import { formatServerDate } from '../utils/date-format';
import { ToastService } from '../services/toast.service';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-details-page.component.html',
})
export class ProductDetailsPageComponent {
  private readonly statusLabels = [
    'Draft',
    'InEnrichment',
    'ReadyForReview',
    'Approved',
    'Published',
    'Rejected',
    'Archived',
  ];

  protected readonly mode = signal<'admin' | 'customer'>('admin');
  protected readonly product = signal<Product | null>(null);
  protected readonly workflow = signal<WorkflowSnapshot>({});
  protected form: any = null;
  protected message = '';
  protected readonly user = this.authService.user;
  protected readonly isAuthenticated = this.authService.isAuthenticated;
  protected readonly isCustomer = computed(() => this.authService.user()?.role === 'Customer');
  protected readonly canEdit = computed(() =>
    this.mode() === 'admin' &&
    ['Admin', 'ProductManager', 'ContentExecutive'].includes(this.authService.user()?.role ?? '')
  );
  protected readonly canDelete = computed(() =>
    this.mode() === 'admin' &&
    ['Admin', 'ProductManager'].includes(this.authService.user()?.role ?? '')
  );
  protected readonly isAdmin = computed(() => this.authService.user()?.role === 'Admin');
  protected readonly canSubmitForReview = computed(() => this.authService.user()?.role !== 'ContentExecutive');

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly catalogService: CatalogService,
    private readonly apiService: ApiService,
    private readonly http: HttpClient,
    private readonly authService: AuthService,
    private readonly cartService: CartService,
    private readonly toastService: ToastService
  ) {
    const mode = (this.route.snapshot.data['mode'] as 'admin' | 'customer') ?? 'admin';
    this.mode.set(mode);
    void this.loadData();
  }

  protected addToCart(): void {
    if (!this.isAuthenticated()) {
      this.message = 'Please sign in as a customer to add items to cart.';
      return;
    }

    if (!this.isCustomer()) {
      this.message = 'Only customer accounts can add items to cart.';
      return;
    }

    if (this.product()) {
      this.cartService.addItem(this.product()!);
      this.message = 'Added to preview cart.';
    }
  }

  protected async saveProduct(): Promise<void> {
    await firstValueFrom(this.http.put(`/catalog/products/${this.product()!.id}`, this.form));
    await this.saveAudit('ProductUpdated', `${this.form.name} metadata updated.`);
    this.message = 'Product details saved.';
    this.toastService.show(`Product "${this.form.name}" details saved successfully.`);
    await this.loadData();
  }

  private async savePricing(): Promise<void> {
    await firstValueFrom(
      this.http.put(`/workflow/pricing/${this.product()!.id}`, {
        mrp: Number(this.form.sellingPrice || 0) * 1.15,
        sellingPrice: Number(this.form.sellingPrice || 0),
      })
    );
    await this.saveAudit('PricingUpdated', `Pricing updated for ${this.form.name}.`);
    this.message = 'Pricing updated.';
    await this.loadData();
  }

  private async saveInventory(): Promise<void> {
    await firstValueFrom(
      this.http.put(`/workflow/inventory/${this.product()!.id}`, {
        quantity: Number(this.form.stockQuantity || 0),
        availabilityMessage: Number(this.form.stockQuantity || 0) > 0 ? 'In Stock' : 'Out of Stock',
      })
    );
    await this.saveAudit('InventoryUpdated', `Inventory updated for ${this.form.name}.`);
    this.message = 'Inventory updated.';
    await this.loadData();
  }

  protected async submitForReview(): Promise<void> {
    await this.saveProduct();
    await this.savePricing();
    await this.saveInventory();
    await firstValueFrom(this.http.post(`/workflow/submit/${this.product()!.id}`, {}));
    await firstValueFrom(
      this.http.put(`/catalog/products/${this.product()!.id}/status`, {
        status: 'ReadyForReview',
        isPublished: false,
      })
    );
    await this.saveAudit('SubmittedForReview', `${this.form.name} submitted for approval.`);
    this.message = 'Product submitted for admin review.';
    this.toastService.show(`Product "${this.form.name}" submitted for review successfully.`);
    await this.loadData();
  }

  protected async deleteProduct(): Promise<void> {
    const product = this.product();
    if (!product) {
      return;
    }

    const confirmed = window.confirm(`Delete "${product.name}"? This action cannot be undone.`);
    if (!confirmed) {
      return;
    }

    try {
      await firstValueFrom(this.apiService.deleteWorkflowProductData(product.id));
      await firstValueFrom(this.catalogService.deleteAdminProduct(product.id));
      await this.saveAudit('ProductDeleted', `${product.name} was deleted from the admin console.`);
      this.toastService.show(`Product "${product.name}" deleted successfully.`);
      await this.router.navigate(['/admin/products']);
    } catch {
      this.message = 'Unable to delete product.';
      this.toastService.show('Unable to delete product.', 'error');
    }
  }

  protected async reviewAction(action: 'approve' | 'reject' | 'publish', status: string, auditAction: string): Promise<void> {
    await firstValueFrom(
      this.http.post(`/workflow/${action}/${this.product()!.id}`, {
        remarks: `${status} by admin`,
      })
    );
    await firstValueFrom(
      this.http.put(`/catalog/products/${this.product()!.id}/status`, {
        status,
        isPublished: status === 'Published',
      })
    );
    await this.saveAudit(auditAction, `${this.form.name} marked as ${status}.`);
    this.message = `Product ${status.toLowerCase()}.`;
    this.toastService.show(`Product "${this.form.name}" ${status.toLowerCase()} successfully.`);
    await this.loadData();
  }

  protected get statusLabel(): string {
    const status = this.product()?.status;
    if (typeof status === 'number') {
      return this.statusLabels[status] ?? 'Unknown';
    }

    return status || 'Unknown';
  }

  protected get statusClass(): string {
    return `pill status-${this.statusLabel.toLowerCase()}`;
  }

  protected get statusTimestamp(): string {
    const product = this.product();
    return formatServerDate(product?.updatedAt || product?.createdAt);
  }

  private async loadData(): Promise<void> {
    const id = this.route.snapshot.paramMap.get('id')!;
    const product =
      this.mode() === 'customer'
        ? await firstValueFrom(this.catalogService.getCustomerProduct(id))
        : await firstValueFrom(this.catalogService.getAdminProduct(id));

    const workflow = this.authService.isAuthenticated()
      ? await firstValueFrom(this.apiService.getWorkflow(id)).catch(() => ({}))
      : {};

    this.product.set(product);
    this.form = { ...product };
    this.workflow.set(workflow);
  }

  private async saveAudit(action: string, details: string): Promise<void> {
    if (!this.authService.isAuthenticated()) {
      return;
    }

    await firstValueFrom(
      this.http.post('/admin/audit', {
        productId: this.product()!.id,
        action,
        entityName: this.mode() === 'customer' ? 'Storefront' : 'Admin',
        details,
      })
    );
  }
}
