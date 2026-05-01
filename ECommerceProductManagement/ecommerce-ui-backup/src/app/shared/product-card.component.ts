import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../models';
import { formatServerDate } from '../utils/date-format';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-card.component.html',
})
export class ProductCardComponent {
  private readonly statusLabels = [
    'Draft',
    'InEnrichment',
    'ReadyForReview',
    'Approved',
    'Published',
    'Rejected',
    'Archived',
  ];

  @Input({ required: true }) product!: Product;
  @Input() mode: 'admin' | 'customer' = 'admin';

  constructor(private readonly router: Router) {}

  protected open(): void {
    const destination =
      this.mode === 'customer'
        ? `/customer/products/${this.product.id}`
        : `/admin/products/${this.product.id}`;
    void this.router.navigateByUrl(destination);
  }

  protected get rating(): string {
    return (4 + ((this.product?.sellingPrice || 0) % 10) / 20).toFixed(1);
  }

  protected get reviewCount(): number {
    return 120 + ((this.product?.stockQuantity || 0) * 7);
  }

  protected get statusLabel(): string {
    const status = this.product?.status;
    if (typeof status === 'number') {
      return this.statusLabels[status] ?? 'Unknown';
    }

    return status || 'Unknown';
  }

  protected get statusClass(): string {
    return `pill status-${this.statusLabel.toLowerCase()}`;
  }

  protected get statusTimestamp(): string {
    return formatServerDate(this.product?.updatedAt || this.product?.createdAt);
  }
}
