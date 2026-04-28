import { CommonModule } from '@angular/common';
import { Component, computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CartService } from '../services/cart.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './cart-preview-page.component.html',
})
export class CartPreviewPageComponent {
  protected readonly items = this.cartService.items;
  protected readonly totalItems = this.cartService.totalItems;
  protected readonly totalPrice = this.cartService.totalPrice;
  protected readonly estimatedTax = computed(() => Math.round(this.totalPrice() * 0.18));
  protected readonly grandTotal = computed(() => this.totalPrice() + this.estimatedTax());
  protected readonly isCustomer = computed(() => this.authService.user()?.role === 'Customer');

  constructor(
    private readonly cartService: CartService,
    private readonly authService: AuthService
  ) {}

  protected addItem(item: any): void {
    if (!this.isCustomer()) {
      return;
    }

    this.cartService.addItem(item);
  }

  protected removeItem(productId: string): void {
    if (!this.isCustomer()) {
      return;
    }

    this.cartService.removeItem(productId);
  }

  protected decreaseItem(productId: string): void {
    if (!this.isCustomer()) {
      return;
    }

    this.cartService.decreaseItem(productId);
  }
}
