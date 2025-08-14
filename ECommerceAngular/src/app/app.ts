import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { Catalog } from './components/catalog.component';
import { AuthService } from './services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Catalog, CommonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  constructor(public authService: AuthService, private router: Router) {}

  goLogin() {
    const role = this.authService.getRole();
    this.router.navigate([`/${role === 'Admin' ? 'admin' : 'public'}/login`]);
  }

  goRegister() {
    const role = this.authService.getRole();
    this.router.navigate([`/${role === 'Admin' ? 'admin' : 'public'}/register`]);
  }

  goOrders() {
    const role = this.authService.getRole();
    this.router.navigate([`/${role === 'Admin' ? 'admin' : 'public'}/orders`]);
  }

  goProducts() {
    const role = this.authService.getRole();
    this.router.navigate([`/${role === 'Admin' ? 'admin' : 'public'}/products`]);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/public/login']);
  }
}
