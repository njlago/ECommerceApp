import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Catalog } from "./components/catalog.component";
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Catalog, CommonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  constructor(public authService: AuthService, private router: Router) {}
  goLogin() {
    // admin condition
    this.router.navigate(['/public/login'])
  }
  goRegister() {
    // admin condition
    this.router.navigate(['/public/register'])
  }
  logout()
  {
    this.authService.logout();
    this.router.navigate(['/public/login']);
  }
}
