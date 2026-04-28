import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CatalogService } from '../services/catalog.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Product } from '../models';
import { ProductCardComponent } from '../shared/product-card.component';

const emptyProduct = {
  name: '',
  sku: '',
  brand: '',
  description: '',
  categoryName: '',
  seoTitle: '',
  seoDescription: '',
  tags: '',
  primaryImageUrl: '',
  sellingPrice: 0,
  stockQuantity: 0,
};

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, ProductCardComponent],
  templateUrl: './product-list-page.component.html',
})
export class ProductListPageComponent {
  protected readonly products = signal<Product[]>([]);
  protected readonly query = signal('');
  protected form = { ...emptyProduct };
  protected error = '';
  protected saving = false;
  protected readonly user = this.authService.user;
  protected readonly filteredProducts = computed(() => {
    const value = this.query().trim().toLowerCase();
    if (!value) {
      return this.products();
    }

    return this.products().filter((product) =>
      [product.name, product.sku, product.brand, product.categoryName, product.tags]
        .filter(Boolean)
        .some((field) => field.toLowerCase().includes(value))
    );
  });

  constructor(
    private readonly catalogService: CatalogService,
    private readonly http: HttpClient,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    void this.loadProducts();
  }

  protected async onCreate(): Promise<void> {
    this.saving = true;
    this.error = '';

    try {
      const data = await firstValueFrom(this.http.post<Product>('/catalog/products', this.form));
      await firstValueFrom(
        this.http.post('/admin/audit', {
          productId: data.id,
          action: 'ProductCreated',
          entityName: 'Catalog',
          details: `${data.name} was created from the admin product console.`,
        })
      );
      this.form = { ...emptyProduct };
      await this.loadProducts();
      await this.router.navigate(['/admin/products', data.id]);
    } catch (error: any) {
      this.error = error?.error || 'Unable to create product.';
    } finally {
      this.saving = false;
    }
  }

  private async loadProducts(): Promise<void> {
    const data = await firstValueFrom(this.catalogService.getAdminProducts());
    this.products.set(data);
  }
}
