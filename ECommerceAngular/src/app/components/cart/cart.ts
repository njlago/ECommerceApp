import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CartItem } from '../../models/cart-item';
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
    this.loadItems();
  }

  loadItems() {
    this.cartService.getItems().subscribe(data => {
      this.items = data;
    });
  }

  updateQuantity(item: CartItem) {
    this.cartService.updateItem(item.productId, item.quantity).subscribe(() => {
      this.loadItems();
    });
  }

  removeItem(productId: number) {
    this.cartService.removeItem(productId).subscribe(() => {
      this.loadItems();
    });
  }

  clearCart() {
    this.cartService.clearCart().subscribe(() => {
      this.items = [];
    });
  }
}
