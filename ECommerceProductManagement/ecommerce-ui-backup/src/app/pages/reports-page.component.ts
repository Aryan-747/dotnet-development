import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../services/api.service';
import { CatalogService } from '../services/catalog.service';
import { AuditLog, DashboardSummary, Product } from '../models';
import { formatServerDate } from '../utils/date-format';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reports-page.component.html',
})
export class ReportsPageComponent {
  protected readonly products = signal<Product[]>([]);
  protected selectedProductId = '';
  protected readonly logs = signal<AuditLog[]>([]);
  protected readonly dashboard = signal<DashboardSummary | null>(null);

  constructor(
    private readonly catalogService: CatalogService,
    private readonly apiService: ApiService,
    private readonly http: HttpClient
  ) {
    void this.load();
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

  protected async onProductChange(): Promise<void> {
    if (!this.selectedProductId) {
      return;
    }

    this.logs.set(await firstValueFrom(this.apiService.getAudit(this.selectedProductId)));
  }

  protected async exportAudit(): Promise<void> {
    const response = await firstValueFrom(
      this.http.get(`/admin/export/audit/${this.selectedProductId}`, { responseType: 'blob' })
    );

    const url = window.URL.createObjectURL(response);
    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', `audit-${this.selectedProductId}.csv`);
    document.body.appendChild(link);
    link.click();
    link.remove();
  }

  private async load(): Promise<void> {
    const [products, dashboard] = await Promise.all([
      firstValueFrom(this.catalogService.getAdminProducts()),
      firstValueFrom(this.apiService.getDashboard()),
    ]);

    this.products.set(products);
    this.dashboard.set(dashboard);
    if (products[0]) {
      this.selectedProductId = products[0].id;
      await this.onProductChange();
    }
  }
}
