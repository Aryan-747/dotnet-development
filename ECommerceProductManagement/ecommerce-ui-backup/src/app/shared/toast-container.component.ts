import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ToastService } from '../services/toast.service';

@Component({
  selector: 'app-toast-container',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="toast-stack" aria-live="polite" aria-atomic="true">
      @for (toast of toastService.toasts(); track toast.id) {
        <div class="toast" [class.toast-error]="toast.variant === 'error'">
          <span>{{ toast.text }}</span>
          <button type="button" class="toast-close" (click)="toastService.dismiss(toast.id)" aria-label="Dismiss notification">
            ×
          </button>
        </div>
      }
    </div>
  `,
})
export class ToastContainerComponent {
  constructor(protected readonly toastService: ToastService) {}
}
