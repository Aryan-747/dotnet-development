import { Injectable, computed, signal } from '@angular/core';
import { CART_STORAGE_KEY } from '../app.constants';
import { CartItem, Product } from '../models';

@Injectable({ providedIn: 'root' })
export class CartService {
  private readonly itemsSignal = signal<CartItem[]>(this.readCart());

  readonly items = computed(() => this.itemsSignal());
  readonly totalItems = computed(() =>
    this.itemsSignal().reduce((sum, item) => sum + Number(item.quantity || 0), 0)
  );
  readonly totalPrice = computed(() =>
    this.itemsSignal().reduce(
      (sum, item) => sum + Number(item.sellingPrice || 0) * Number(item.quantity || 0),
      0
    )
  );
  readonly estimatedTax = computed(() => Math.round(this.totalPrice() * 0.18));
  readonly grandTotal = computed(() => this.totalPrice() + this.estimatedTax());

  constructor() {
    window.addEventListener('storage', () => this.itemsSignal.set(this.readCart()));
  }

  addItem(product: Product | CartItem): void {
    const items = [...this.itemsSignal()];
    const existing = items.find((item) => item.id === product.id);

    if (existing) {
      existing.quantity += 1;
    } else {
      items.push({ ...product, quantity: 1 });
    }

    this.writeCart(items);
  }

  removeItem(productId: string): void {
    this.writeCart(this.itemsSignal().filter((item) => item.id !== productId));
  }

  decreaseItem(productId: string): void {
    const next = this.itemsSignal()
      .map((item) =>
        item.id === productId ? { ...item, quantity: Math.max(0, item.quantity - 1) } : item
      )
      .filter((item) => item.quantity > 0);

    this.writeCart(next);
  }

  clear(): void {
    this.writeCart([]);
  }

  private writeCart(items: CartItem[]): void {
    this.itemsSignal.set(items);
    localStorage.setItem(CART_STORAGE_KEY, JSON.stringify(items));
  }

  private readCart(): CartItem[] {
    const raw = localStorage.getItem(CART_STORAGE_KEY);
    return raw ? JSON.parse(raw) : [];
  }
}
