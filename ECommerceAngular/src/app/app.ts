import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Catalog } from "./components/catalog.component";
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Catalog, HttpClientModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  constructor(private router: Router) {}
  goLogin() {
    this.router.navigate(['/public/login'])
  }
  goRegister() {
    this.router.navigate(['/public/register'])
  }
}
