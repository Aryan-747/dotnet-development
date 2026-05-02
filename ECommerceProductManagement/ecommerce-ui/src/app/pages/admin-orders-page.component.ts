import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { RouterLink } from '@angular/router';
import { Order } from '../models';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './admin-orders-page.component.html',
})
export class AdminOrdersPageComponent implements OnInit {
  protected orders: Order[] = [];
  protected loading = true;
  protected statuses = ['Placed', 'Dispatched', 'OutForDelivery', 'Delivered', 'Cancelled'];

  constructor(private readonly http: HttpClient) {}

  async ngOnInit() {
    await this.loadOrders();
  }

  async loadOrders() {
    this.loading = true;
    try {
      this.orders = await firstValueFrom(this.http.get<Order[]>('/orders/admin'));
    } catch (e) {
      console.error(e);
    } finally {
      this.loading = false;
    }
  }

  async updateStatus(orderId: string, newStatus: string) {
    try {
      await firstValueFrom(this.http.put(`/orders/${orderId}/status`, { status: newStatus }));
      // Optional: show toast
    } catch (e) {
      console.error(e);
      await this.loadOrders(); // revert on failure
    }
  }
}
