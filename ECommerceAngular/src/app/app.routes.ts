import { Routes } from '@angular/router';
import { RegisterComponent } from './public/register/register';
import { LoginComponent } from './public/login/login';

export const routes: Routes = [
  { path: 'public/register', component: RegisterComponent },
  { path: 'public/login', component: LoginComponent },
  { path: '', redirectTo: 'public/login', pathMatch: 'full' }
];