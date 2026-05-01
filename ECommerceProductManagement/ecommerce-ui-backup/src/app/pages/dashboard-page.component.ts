import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../services/api.service';
import { CatalogService } from '../services/catalog.service';
import { ApprovalQueueItem, AuditLog, DashboardSummary, Product } from '../models';
import { formatServerDate } from '../utils/date-format';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard-page.component.html',
})
export class DashboardPageComponent {
  protected readonly summary = signal<DashboardSummary | null>(null);
  protected readonly products = signal<Product[]>([]);
  protected readonly queue = signal<ApprovalQueueItem[]>([]);
  protected readonly alerts = signal<AuditLog[]>([]);

  constructor(private readonly apiService: ApiService, private readonly catalogService: CatalogService) {
    void this.load();
  }

  protected get publishedCount(): number {
    return this.products().filter((product) => product.isPublished).length;
  }

  protected statusLabel(value: string): string {
    return value || 'Info';
  }

  protected statusClass(value: string): string {
    const normalized = (value || 'info').toLowerCase().replace(/\s+/g, '-');
    return `status-chip status-chip-${normalized}`;
  }

  protected formatTimestamp(value?: string): string {
    return formatServerDate(value);
  }

  private async load(): Promise<void> {
    const [summary, products, queue, alerts] = await Promise.all([
      firstValueFrom(this.apiService.getDashboard()),
      firstValueFrom(this.catalogService.getAdminProducts()),
      firstValueFrom(this.apiService.getWorkflowQueue()),
      firstValueFrom(this.apiService.getAlerts()),
    ]);

    this.summary.set(summary);
    this.products.set(products);
    this.queue.set(queue);
    this.alerts.set(alerts);
  }
}
