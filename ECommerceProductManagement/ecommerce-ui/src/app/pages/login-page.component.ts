import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { AuthResponse } from '../models';
import { GOOGLE_CLIENT_ID } from '../app.constants';

declare global {
  interface Window {
    google?: any;
  }
}

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login-page.component.html',
})
export class LoginPageComponent implements AfterViewInit {
  @ViewChild('googleButtonRef', { static: false }) googleButtonRef?: ElementRef<HTMLDivElement>;

  protected form = { email: '', password: '' };
  protected error = '';
  protected submitting = false;

  constructor(
    private readonly http: HttpClient,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  ngAfterViewInit(): void {
    this.initializeGoogleButton();
  }

  protected async onSubmit(): Promise<void> {
    this.error = '';
    this.submitting = true;

    try {
      const data = await firstValueFrom(this.http.post<AuthResponse>('/auth/login', this.form));
      this.authService.login(data);
      await this.router.navigate([data.user.role === 'Admin' ? '/admin/dashboard' : '/admin/products']);
    } catch {
      this.error = 'Invalid email or password.';
    } finally {
      this.submitting = false;
    }
  }

  private initializeGoogleButton(): void {
    if (!GOOGLE_CLIENT_ID || !this.googleButtonRef?.nativeElement) {
      return;
    }

    const render = () => {
      if (!window.google || !this.googleButtonRef?.nativeElement) {
        return;
      }

      window.google.accounts.id.initialize({
        client_id: GOOGLE_CLIENT_ID,
        callback: async (response: { credential: string }) => {
          this.error = '';
          this.submitting = true;

          try {
            const data = await firstValueFrom(
              this.http.post<AuthResponse>('/auth/google', { idToken: response.credential })
            );
            this.authService.login(data);
            await this.router.navigate([data.user.role === 'Admin' ? '/admin/dashboard' : '/admin/products']);
          } catch (error: any) {
            this.error = error?.error || 'Google sign-in failed.';
          } finally {
            this.submitting = false;
          }
        },
      });

      this.googleButtonRef.nativeElement.innerHTML = '';

      window.google.accounts.id.renderButton(this.googleButtonRef.nativeElement, {
        theme: 'outline',
        size: 'large',
        shape: 'pill',
        width: 320,
        text: 'signin_with',
      });
    };

    if (window.google) {
      render();
      return;
    }

    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    script.onload = render;
    document.body.appendChild(script);
  }
}
