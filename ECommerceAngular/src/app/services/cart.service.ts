import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../models/cart-item';

@Injectable({ providedIn: 'root' })
export class CartService {
  private itemsSubject = new BehaviorSubject<CartItem[]>([]);
  private items: CartItem[] = [];

  getItems(): Observable<CartItem[]> {
    return this.itemsSubject.asObservable();
  }

  addItem(item: CartItem): void {
    const existing = this.items.find(i => i.productId === item.productId);
    if (existing) {
      existing.quantity += item.quantity;
    } else {
      this.items.push({ ...item });
    }
    this.itemsSubject.next([...this.items]);
  }

  updateItem(productId: number, quantity: number): void {
    const item = this.items.find(i => i.productId === productId);
    if (item) {
      item.quantity = quantity;
      this.itemsSubject.next([...this.items]);
    }
  }

  removeItem(productId: number): void {
    this.items = this.items.filter(i => i.productId !== productId);
    this.itemsSubject.next([...this.items]);
  }

  clearCart(): void {
    this.items = [];
    this.itemsSubject.next([]);
  }
}
