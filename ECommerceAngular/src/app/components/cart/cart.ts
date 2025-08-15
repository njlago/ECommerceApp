import { Component, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { CartItem } from '../../models/cart-item';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule, CurrencyPipe],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.css'],
  providers: [CartService, AuthService]
})
export class CartComponent implements OnInit {
  items: CartItem[] = [];

  constructor(private cartService: CartService, private authService: AuthService) {}

  ngOnInit(): void {
    this.cartService.getItems().subscribe(data => this.items = data.filter(item => item.customerId == this.authService.getCustomerId()));
  }

  updateItem(item: CartItem): void {
    this.cartService.updateItem(item.productId, item.quantity).subscribe();
  }

  removeItem(productId: number): void {
    this.cartService.removeItem(productId).subscribe(() => {
      this.items = this.items.filter(i => i.productId !== productId);
    });
  }

  clearCart(): void {
    this.cartService.clearCart().subscribe(() => {
      this.items = [];
    });
  }
}
