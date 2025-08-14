import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { Product } from '../../models/product.class';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Catalog } from '../catalog.component';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [FormsModule, HttpClientModule, Catalog, CommonModule],
  templateUrl: './customer.orders.component.html',
})
export class CustomerOrdersComponent {
  products: Product[] = [];
  newProduct: Product = { id: 0, name: '', description: '', price: 0, stock: 0, categoryId: 0 };
  editingProduct: Product | null = null;

  constructor(private adminService: AdminService) {}

  
}