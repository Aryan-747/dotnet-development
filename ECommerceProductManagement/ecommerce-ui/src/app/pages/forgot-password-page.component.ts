import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './forgot-password-page.component.html',
})
export class ForgotPasswordPageComponent {
  protected step: 'email' | 'reset' = 'email';
  protected form = { email: '', otpCode: '', newPassword: '' };
  protected error = '';
  protected success = '';
  protected submitting = false;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  protected async onSubmitEmail(): Promise<void> {
    if (!this.form.email) return;

    this.error = '';
    this.success = '';
    this.submitting = true;

    try {
      await firstValueFrom(this.http.post('/auth/forgot-password', { email: this.form.email }));
      this.step = 'reset';
      this.success = 'If the email is registered, a reset code has been sent.';
    } catch {
      this.error = 'An error occurred. Please try again.';
    } finally {
      this.submitting = false;
    }
  }

  protected async onSubmitReset(): Promise<void> {
    if (!this.form.otpCode || !this.form.newPassword) return;

    if (this.form.newPassword.length < 8) {
      this.error = 'Password must be at least 8 characters long.';
      return;
    }

    this.error = '';
    this.success = '';
    this.submitting = true;

    try {
      await firstValueFrom(this.http.post('/auth/reset-password', {
        email: this.form.email,
        otpCode: this.form.otpCode,
        newPassword: this.form.newPassword
      }));
      this.success = 'Password reset successfully! Redirecting...';
      
      setTimeout(() => {
        this.router.navigate(['/auth/login']);
      }, 2000);
    } catch (e: any) {
      this.error = e.error?.message || 'Invalid or expired reset code.';
    } finally {
      this.submitting = false;
    }
  }
}
