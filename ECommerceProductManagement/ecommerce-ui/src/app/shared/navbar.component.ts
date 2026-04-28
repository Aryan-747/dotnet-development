import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NavigationEnd, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  protected searchText = '';
  protected readonly isMenuOpen = signal(false);
  protected readonly totalItems = this.cartService.totalItems;
  protected readonly isAuthenticated = this.authService.isAuthenticated;
  protected readonly user = this.authService.user;
  protected readonly isCustomer = computed(() => this.authService.user()?.role === 'Customer');
  protected readonly canAccessSellerConsole = computed(() =>
    ['Admin', 'ProductManager', 'ContentExecutive'].includes(this.authService.user()?.role ?? '')
  );
  protected readonly isCustomerView = signal(true);

  constructor(
    private readonly authService: AuthService,
    private readonly cartService: CartService,
    private readonly router: Router
  ) {
    this.syncWithRoute(router.url);
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => this.syncWithRoute((event as NavigationEnd).urlAfterRedirects));
  }

  protected onLogout(): void {
    this.isMenuOpen.set(false);
    this.cartService.clear();
    this.authService.logout();
    void this.router.navigate(['/auth/login']);
  }

  protected onSearchSubmit(): void {
    const nextQuery = this.searchText.trim();
    void this.router.navigate(['/customer/products'], {
      queryParams: nextQuery ? { q: nextQuery } : {},
    });
  }

  protected toggleMenu(): void {
    this.isMenuOpen.update((value) => !value);
  }

  private syncWithRoute(url: string): void {
    const [path, search] = url.split('?');
    this.isCustomerView.set(path.startsWith('/customer'));
    const params = new URLSearchParams(search ?? '');
    this.searchText = params.get('q') ?? '';
  }
}
