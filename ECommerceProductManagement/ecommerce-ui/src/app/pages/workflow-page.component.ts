import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../services/api.service';
import { ApprovalQueueItem } from '../models';
import { formatServerDate } from '../utils/date-format';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './workflow-page.component.html',
})
export class WorkflowPageComponent {
  protected readonly queue = signal<ApprovalQueueItem[]>([]);

  constructor(private readonly apiService: ApiService) {
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

  private async load(): Promise<void> {
    this.queue.set(await firstValueFrom(this.apiService.getWorkflowQueue()));
  }
}
