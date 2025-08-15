import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../models/cart-item';
import { CartService } from '../../services/cart.service';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.html',
  styleUrls: ['./cart.css']
})
export class CartComponent implements OnInit {
  items: CartItem[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.cartService.getItems().subscribe({
      next: (data) => this.items = data,
      error: (err) => console.error('Failed to load cart', err)
    });
  }

  updateQuantity(item: CartItem): void {
    this.cartService.updateItem(item.productId, item.quantity).subscribe({
      next: () => this.loadCart(),
      error: (err) => console.error('Update failed', err)
    });
  }

  removeItem(productId: number): void {
    this.cartService.removeItem(productId).subscribe({
      next: () => this.loadCart(),
      error: (err) => console.error('Remove failed', err)
    });
  }

  clearCart(): void {
    this.cartService.clearCart().subscribe({
      next: () => this.items = [],
      error: (err) => console.error('Clear failed', err)
    });
  }
}
