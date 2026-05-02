import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Order } from '../models';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './admin-order-details-page.component.html'
})
export class AdminOrderDetailsPageComponent implements OnInit {
  order?: Order;
  loading = true;
  statuses = ['Placed', 'Dispatched', 'OutForDelivery', 'Delivered', 'Cancelled'];

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.fetchOrder(id);
    }
  }

  fetchOrder(id: string): void {
    this.http.get<Order>(`/orders/${id}`).subscribe({
      next: (data) => {
        this.order = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  updateStatus(status: string): void {
    if (!this.order) return;
    this.http.put(`/orders/${this.order.id}/status`, { status }).subscribe({
      next: () => {
        if (this.order) this.order.status = status;
      }
    });
  }

  deleteOrder(): void {
    if (!this.order || !confirm('Are you sure you want to permanently delete this order?')) return;
    this.http.delete(`/orders/${this.order.id}`).subscribe({
      next: () => {
        alert('Order deleted successfully');
        window.history.back();
      }
    });
  }
}
