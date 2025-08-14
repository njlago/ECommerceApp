import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartItem } from '../../models/cart-item';
import { CartService } from '../../services/cart.service';
import { Observable } from 'rxjs';

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

  ngOnInit() {
    this.cartService.getItems().subscribe(data => {
      this.items = data;
    });
  }

  updateQuantity(item: CartItem) {
    this.cartService.updateItem(item.productId, item.quantity);
    this.cartService.getItems().subscribe(data => {
      this.items = data;
    });
  }

  removeItem(productId: number) {
    this.cartService.removeItem(productId);
    this.cartService.getItems().subscribe(data => {
      this.items = data;
    });
  }

  clearCart() {
    this.cartService.clearCart();
    this.items = [];
  }
}
