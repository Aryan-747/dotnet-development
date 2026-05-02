import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { roleGuard } from './guards/role.guard';
import { LoginPageComponent } from './pages/login-page.component';
import { SignupPageComponent } from './pages/signup-page.component';
import { ForgotPasswordPageComponent } from './pages/forgot-password-page.component';
import { CustomerProductsPageComponent } from './pages/customer-products-page.component';
import { ProductDetailsPageComponent } from './pages/product-details-page.component';
import { CartPreviewPageComponent } from './pages/cart-preview-page.component';
import { ProductListPageComponent } from './pages/product-list-page.component';
import { DashboardPageComponent } from './pages/dashboard-page.component';
import { WorkflowPageComponent } from './pages/workflow-page.component';
import { ReportsPageComponent } from './pages/reports-page.component';
import { CheckoutPageComponent } from './pages/checkout-page.component';
import { OrdersPageComponent } from './pages/orders-page.component';
import { AdminOrdersPageComponent } from './pages/admin-orders-page.component';
import { AdminOrderDetailsPageComponent } from './pages/admin-order-details-page.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'customer/products' },
  { path: 'auth/login', component: LoginPageComponent },
  { path: 'auth/signup', component: SignupPageComponent },
  { path: 'auth/forgot', component: ForgotPasswordPageComponent },
  { path: 'customer/products', component: CustomerProductsPageComponent },
  { path: 'customer/products/:id', component: ProductDetailsPageComponent, data: { mode: 'customer' } },
  {
    path: 'customer/cart',
    component: CartPreviewPageComponent,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'customer/checkout',
    component: CheckoutPageComponent,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'customer/orders',
    component: OrdersPageComponent,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'admin/products',
    component: ProductListPageComponent,
    canActivate: [authGuard, roleGuard(['Admin', 'ProductManager', 'ContentExecutive'])],
  },
  {
    path: 'admin/products/:id',
    component: ProductDetailsPageComponent,
    data: { mode: 'admin' },
    canActivate: [authGuard, roleGuard(['Admin', 'ProductManager', 'ContentExecutive'])],
  },
  { path: 'admin/orders', component: AdminOrdersPageComponent, canActivate: [authGuard, roleGuard(['Admin', 'ProductManager'])] },
  { path: 'admin/orders/:id', component: AdminOrderDetailsPageComponent, canActivate: [authGuard, roleGuard(['Admin', 'ProductManager'])] },
  { path: 'admin/dashboard', component: DashboardPageComponent, canActivate: [authGuard, roleGuard(['Admin'])] },
  { path: 'admin/workflow', component: WorkflowPageComponent, canActivate: [authGuard, roleGuard(['Admin'])] },
  { path: 'admin/reports', component: ReportsPageComponent, canActivate: [authGuard, roleGuard(['Admin'])] },
];
