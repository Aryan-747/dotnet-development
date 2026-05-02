import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

import { Order } from '../models';

@Component({
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders-page.component.html',
})
export class OrdersPageComponent implements OnInit {
  protected orders: Order[] = [];
  protected loading = true;

  constructor(private readonly http: HttpClient) {}

  async ngOnInit() {
    await this.loadOrders();
  }

  async loadOrders() {
    this.loading = true;
    try {
      this.orders = await firstValueFrom(this.http.get<Order[]>('/orders'));
    } catch (e) {
      console.error(e);
    } finally {
      this.loading = false;
    }
  }

  async cancelOrder(orderId: string) {
    if (!confirm('Are you sure you want to cancel this order?')) return;
    try {
      await firstValueFrom(this.http.post(`/orders/${orderId}/cancel`, {}));
      await this.loadOrders();
    } catch (e) {
      console.error(e);
      alert('Failed to cancel order. It may already be processed.');
    }
  }
}
