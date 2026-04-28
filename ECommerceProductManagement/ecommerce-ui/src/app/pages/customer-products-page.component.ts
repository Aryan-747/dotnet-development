import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CatalogService } from '../services/catalog.service';
import { Product } from '../models';
import { ProductCardComponent } from '../shared/product-card.component';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, ProductCardComponent],
  templateUrl: './customer-products-page.component.html',
})
export class CustomerProductsPageComponent {
  protected readonly products = signal<Product[]>([]);
  protected readonly allProducts = signal<Product[]>([]);
  protected readonly loading = signal(true);
  protected readonly query = signal('');
  protected readonly category = signal('All');
  protected readonly sortBy = signal('featured');
  protected readonly categories = computed(() => [
    'All',
    ...new Set(this.allProducts().map((product) => product.categoryName).filter(Boolean)),
  ]);

  constructor(
    private readonly catalogService: CatalogService,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {
    this.route.queryParamMap.subscribe((params) => {
      this.query.set(params.get('q') ?? '');
      this.category.set(params.get('category') ?? 'All');
      this.sortBy.set(params.get('sort') ?? 'featured');
      void this.loadProducts();
    });

    void this.loadCategories();
  }

  protected updateParams(next: Record<string, string>): void {
    const params = { ...this.route.snapshot.queryParams };

    Object.entries(next).forEach(([key, value]) => {
      if (!value || value === 'All' || value === 'featured') {
        delete params[key];
      } else {
        params[key] = value;
      }
    });

    void this.router.navigate([], {
      relativeTo: this.route,
      queryParams: params,
    });
  }

  private async loadProducts(): Promise<void> {
    this.loading.set(true);
    const data = await firstValueFrom(
      this.catalogService.searchPublished({
        q: this.query() || undefined,
        category: this.category() === 'All' ? undefined : this.category(),
        sort: this.sortBy() === 'featured' ? undefined : this.sortBy(),
      })
    );
    this.products.set(data);
    this.loading.set(false);
  }

  private async loadCategories(): Promise<void> {
    const data = await firstValueFrom(this.catalogService.getPublished());
    this.allProducts.set(data);
  }
}
