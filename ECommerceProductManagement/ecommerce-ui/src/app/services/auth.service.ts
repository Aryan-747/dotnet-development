import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { AUTH_STORAGE_KEY } from '../app.constants';
import { AuthResponse, UserProfile } from '../models';

interface SessionState {
  token: string;
  user: UserProfile | null;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly sessionSignal = signal<SessionState>(this.readSession());
  private readonly readySignal = signal(false);

  readonly token = computed(() => this.sessionSignal().token);
  readonly user = computed(() => this.sessionSignal().user);
  readonly ready = computed(() => this.readySignal());
  readonly isAuthenticated = computed(() => !!this.sessionSignal().token);

  constructor(private readonly http: HttpClient) {}

  async restoreSession(): Promise<void> {
    const token = this.sessionSignal().token;

    if (!token) {
      this.readySignal.set(true);
      return;
    }

    try {
      const user = await firstValueFrom(this.http.get<UserProfile>('/auth/me'));
      this.setSession({ token, user });
    } catch {
      localStorage.removeItem(AUTH_STORAGE_KEY);
      this.sessionSignal.set({ token: '', user: null });
    } finally {
      this.readySignal.set(true);
    }
  }

  login(payload: { token: string; user: UserProfile }): void {
    this.setSession(payload);
  }

  logout(): void {
    localStorage.removeItem(AUTH_STORAGE_KEY);
    this.sessionSignal.set({ token: '', user: null });
  }

  private setSession(payload: { token: string; user: UserProfile | null }): void {
    const next = { token: payload.token, user: payload.user };
    this.sessionSignal.set(next);
    localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(next));
  }

  private readSession(): SessionState {
    const raw = localStorage.getItem(AUTH_STORAGE_KEY);
    return raw ? JSON.parse(raw) : { token: '', user: null };
  }
}
