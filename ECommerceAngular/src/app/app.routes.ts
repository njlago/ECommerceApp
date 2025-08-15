import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login';
import { Catalog } from './components/catalog.component';
import { CategoriesComponent } from './components/admin/categories/categories';
import { AuthGuard } from './services/auth.guard';
import { NoAuthGuard } from './services/no.auth.guard';
import { AdminGuard } from './services/admin.guard';
import { AdminOrdersComponent } from './components/admin/orders/admin.orders.component';
import { CustomerOrdersComponent } from './components/public/customer.orders.component';
import { CartComponent } from './components/cart/cart';
import { AdminProductsComponent } from './components/admin/products/admin.products';

export const routes: Routes = [
  { path: 'public/register', component: RegisterComponent },
  { path: 'public/login', component: LoginComponent, canActivate: [NoAuthGuard] },
  { path: '', redirectTo: 'public/products', pathMatch: 'full' },
  { path: 'public/products', component: Catalog },
  { path: 'admin/products', component: AdminProductsComponent, canActivate: [AdminGuard] },
  { path: 'public/orders', component: CustomerOrdersComponent },
  { path: 'admin/orders', component: AdminOrdersComponent },
  { path: 'admin/login', component: LoginComponent },
  { path: 'admin/register', component: RegisterComponent },
  { path: 'admin/categories', component: CategoriesComponent, canActivate: [AuthGuard, AdminGuard] },
  { path: 'cart', component: CartComponent }

];

