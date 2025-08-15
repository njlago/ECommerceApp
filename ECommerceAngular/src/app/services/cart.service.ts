import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CartItem } from '../models/cart-item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5146/api/cart';

  constructor(private http: HttpClient) {}

  getItems(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(this.apiUrl);
  }

  addItem(item: CartItem): Observable<any> {
    return this.http.post(this.apiUrl, item);
  }

  updateItem(productId: number, quantity: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${productId}`, quantity);
  }

  removeItem(productId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${productId}`);
  }

  clearCart(): Observable<any> {
    return this.http.delete(this.apiUrl);
  }
}
