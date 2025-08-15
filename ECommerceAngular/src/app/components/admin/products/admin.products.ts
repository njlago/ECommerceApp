import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';
import { Product } from '../../../models/product.class';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Catalog } from '../../catalog.component';
import { CommonModule, NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [FormsModule, HttpClientModule, Catalog, CommonModule],
  templateUrl: './admin.products.html',
})
export class AdminProductsComponent implements OnInit {
  products: Product[] = [];
  newProduct: Product = { id: 0, name: '', description: '', price: 0, stock: 0, categoryId: 1 };
  delProduct: Product = { id: 0, name: '', description: '', price: 0, stock: 0, categoryId: 1 };
  editingProduct: Product | null = null;

  constructor(private productService: ProductService, private adminService: AdminService, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe(data => {

      this.products = data;
    });
  }

  loadProducts() {
    this.productService.getProducts().subscribe(data => this.products = data);
  }

  addProduct() {
    this.adminService.addProduct(this.newProduct).subscribe(() => this.loadProducts());
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    this.router.navigate(['admin/products']);
    
});



  }

  editProduct(product: Product) {
    this.editingProduct = { ...product };
  }

  updateProduct() {
    if (this.editingProduct) {
      

      this.adminService.updateProduct(this.editingProduct).subscribe(() => this.loadProducts());
      this.editingProduct = null;
    // this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    // this.router.navigate(['admin/products']);
      // });
    }
  }

  deleteProduct() {
      
  
    this.adminService.deleteProduct(this.delProduct.id).subscribe(() => this.loadProducts());
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
  this.router.navigate(['admin/products']);
});


  }
}