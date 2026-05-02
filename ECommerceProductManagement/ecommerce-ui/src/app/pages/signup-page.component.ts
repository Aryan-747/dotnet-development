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

  private validateForm(): boolean {
    this.error = '';

    if (!this.form.name || this.form.name.trim().length < 2) {
      this.error = 'Please enter a valid full name (minimum 2 characters).';
      return false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.form.email)) {
      this.error = 'Please enter a valid email address.';
      return false;
    }

    if (this.form.password.length < 8) {
      this.error = 'Password must be at least 8 characters long.';
      return false;
    }

    const hasLetter = /[a-zA-Z]/.test(this.form.password);
    const hasNumber = /\d/.test(this.form.password);
    const hasSpecial = /[\W_]/.test(this.form.password);

    if (!hasLetter || !hasNumber || !hasSpecial) {
      this.error = 'Password must contain at least one letter, one number, and one special character.';
      return false;
    }

    if (this.form.password !== this.form.confirmPassword) {
      this.error = 'Passwords do not match.';
      return false;
    }

    return true;
  }

  protected async onSubmit(): Promise<void> {
    this.message = '';
    
    if (!this.validateForm()) {
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
