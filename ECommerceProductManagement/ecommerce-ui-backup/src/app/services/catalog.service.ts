import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product } from '../models';

@Injectable({ providedIn: 'root' })
export class CatalogService {
  constructor(private readonly http: HttpClient) {}

  getPublished() {
    return this.http.get<Product[]>('/catalog/products/preview');
  }

  searchPublished(params: {
    q?: string;
    category?: string;
    sort?: string;
  }) {
    let httpParams = new HttpParams();

    if (params.q?.trim()) {
      httpParams = httpParams.set('q', params.q.trim());
    }

    if (params.category?.trim()) {
      httpParams = httpParams.set('category', params.category.trim());
    }

    if (params.sort?.trim()) {
      httpParams = httpParams.set('sort', params.sort.trim());
    }

    return this.http.get<Product[]>('/catalog/products/preview/search', {
      params: httpParams,
    });
  }

  getCustomerProduct(id: string) {
    return this.http.get<Product>(`/catalog/products/preview/${id}`);
  }

  getAdminProduct(id: string) {
    return this.http.get<Product>(`/catalog/products/${id}`);
  }

  getAdminProducts() {
    return this.http.get<Product[]>('/catalog/products');
  }

  deleteAdminProduct(id: string) {
    return this.http.delete<void>(`/catalog/products/${id}`);
  }
}
