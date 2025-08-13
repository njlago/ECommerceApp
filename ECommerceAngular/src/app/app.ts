import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Catalog } from "./components/catalog.component";
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Catalog, HttpClientModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('ECommerceAngular');
}
