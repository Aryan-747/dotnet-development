import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { CartService } from '../services/cart.service';

interface Address {
  id: string;
  fullName: string;
  streetAddress: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  phoneNumber: string;
  isDefault: boolean;
}

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout-page.component.html',
})
export class CheckoutPageComponent implements OnInit {
  protected addresses: Address[] = [];
  protected selectedAddressId: string | null = null;
  protected showAddAddress = false;
  
  protected newAddress: Partial<Address> = {
    fullName: '',
    streetAddress: '',
    city: '',
    state: '',
    postalCode: '',
    country: '',
    phoneNumber: ''
  };

  protected placingOrder = false;
  protected error = '';

  constructor(
    private readonly http: HttpClient,
    protected readonly cartService: CartService,
    private readonly router: Router
  ) {}

  async ngOnInit() {
    if (this.cartService.items().length === 0) {
      this.router.navigate(['/customer/products']);
      return;
    }
    await this.loadAddresses();
  }

  async loadAddresses() {
    try {
      this.addresses = await firstValueFrom(this.http.get<Address[]>('/addresses'));
      const defaultAddr = this.addresses.find(a => a.isDefault);
      if (defaultAddr) {
        this.selectedAddressId = defaultAddr.id;
      } else if (this.addresses.length > 0) {
        this.selectedAddressId = this.addresses[0].id;
      } else {
        this.showAddAddress = true;
      }
    } catch (e) {
      console.error(e);
    }
  }

  async saveAddress() {
    try {
      const added = await firstValueFrom(this.http.post<Address>('/addresses', this.newAddress));
      this.addresses.push(added);
      this.selectedAddressId = added.id;
      this.showAddAddress = false;
    } catch (e) {
      this.error = 'Failed to save address';
    }
  }

  async placeOrder() {
    if (!this.selectedAddressId) {
      this.error = 'Please select an address';
      return;
    }

    this.placingOrder = true;
    this.error = '';

    const payload = {
      addressId: this.selectedAddressId,
      items: this.cartService.items().map(i => ({
        productId: i.id,
        productName: i.name,
        unitPrice: i.sellingPrice || 0,
        quantity: i.quantity
      }))
    };

    try {
      await firstValueFrom(this.http.post('/orders', payload));
      this.cartService.clear();
      this.router.navigate(['/customer/orders']);
    } catch (e) {
      this.error = 'Failed to place order';
      this.placingOrder = false;
    }
  }
}
