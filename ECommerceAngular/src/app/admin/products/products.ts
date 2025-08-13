import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { Product } from '../../models/product.class';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './products.html',
  styleUrls: ['./products.css']
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  newProduct: Product = { id: 0, name: '', description: '', price: 0, stock: 0, categoryId: 0 };
  editingProduct: Product | null = null;

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.adminService.getProducts().subscribe(data => this.products = data);
  }

  addProduct() {
    this.adminService.addProduct(this.newProduct).subscribe(() => {
      this.loadProducts();
      this.newProduct = { id: 0, name: '', description: '', price: 0, stock: 0, categoryId: 0 };
    });
  }

  editProduct(product: Product) {
    this.editingProduct = { ...product };
  }

  updateProduct() {
    if (this.editingProduct) {
      this.adminService.updateProduct(this.editingProduct).subscribe(() => {
        this.loadProducts();
        this.editingProduct = null;
      });
    }
  }

  deleteProduct(id: number) {
    this.adminService.deleteProduct(id).subscribe(() => this.loadProducts());
  }
}