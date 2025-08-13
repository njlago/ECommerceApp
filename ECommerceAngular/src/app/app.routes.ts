import { Routes } from '@angular/router';
import { RegisterComponent } from './public/register/register.component';
import { LoginComponent } from './public/login/login';
import { Catalog } from './components/catalog.component';
import { CategoriesComponent } from './admin/categories/categories';

export const routes: Routes = [
  { path: 'public/register', component: RegisterComponent },
  { path: 'public/login', component: LoginComponent },
  { path: '', redirectTo: 'public/login', pathMatch: 'full' },
  {path: 'public/catalog', component:Catalog},
  { path: 'admin/categories', component: CategoriesComponent }
];
