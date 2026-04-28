import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './signup-page.component.html',
})
export class SignupPageComponent {
  protected form = {
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
    role: 'Customer',
  };
  protected message = '';
  protected error = '';
  protected submitting = false;

  constructor(private readonly http: HttpClient, private readonly router: Router) {}

  protected async onSubmit(): Promise<void> {
    this.error = '';
    this.message = '';

    if (this.form.password !== this.form.confirmPassword) {
      this.error = 'Password and confirm password must match.';
      return;
    }

    this.submitting = true;

    try {
      await firstValueFrom(
        this.http.post('/auth/signup', {
          name: this.form.name,
          email: this.form.email,
          password: this.form.password,
          role: this.form.role,
        })
      );

      this.message = 'Account created. Redirecting to login...';
      setTimeout(() => void this.router.navigate(['/auth/login']), 900);
    } catch (error: any) {
      this.error = error?.error || 'Unable to create account.';
    } finally {
      this.submitting = false;
    }
  }
}
