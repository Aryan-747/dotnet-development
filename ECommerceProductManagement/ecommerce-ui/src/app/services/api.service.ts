import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApprovalQueueItem, AuditLog, DashboardSummary, WorkflowSnapshot } from '../models';

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private readonly http: HttpClient) {}

  getWorkflow(productId: string) {
    return this.http.get<WorkflowSnapshot>(`/workflow/product/${productId}`);
  }

  getWorkflowQueue() {
    return this.http.get<ApprovalQueueItem[]>('/workflow/queue');
  }

  getDashboard() {
    return this.http.get<DashboardSummary>('/admin/dashboard');
  }

  getAlerts() {
    return this.http.get<AuditLog[]>('/admin/alerts');
  }

  getAudit(productId: string) {
    return this.http.get<AuditLog[]>(`/admin/audit/${productId}`);
  }
}
