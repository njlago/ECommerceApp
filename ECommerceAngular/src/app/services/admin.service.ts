import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.class';
import { Category } from '../models/category';


@Injectable({ providedIn: 'root' })
export class AdminService {
  private apiUrl = 'http://localhost:5146/api/admin';

  constructor(private http: HttpClient) {}

  // Product CRUD
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`api/products`);
  }
  addProduct(product: Product): Observable<Product> {


    return this.http.post<Product>(`${this.apiUrl}/products`, product);
  }
  updateProduct(product: Product): Observable<Product> {

    console.log(this.http.put<Product>(`${this.apiUrl}/products/${product.id}`, product));

    return this.http.put<Product>(`${this.apiUrl}/products/${product.id}`, product);
  }
  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/products/${id}`);
  }

  // Category CRUD
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}/categories`);
  }
  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(`${this.apiUrl}/categories`, category);
  }
  updateCategory(category: Category): Observable<Category> {
    return this.http.put<Category>(`${this.apiUrl}/categories/${category.id}`, category);
  }
  deleteCategory(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/categories/${id}`);
  }
}