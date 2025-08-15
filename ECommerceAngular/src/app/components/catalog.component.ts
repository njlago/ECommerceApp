import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ProductService } from "../services/product.service";
import { CartService } from "../services/cart.service";
import { Product } from "../models/product.class";
import { CartItem } from "../models/cart-item";
import { AuthService } from "../services/auth.service";

@Component({
  selector: 'app-catalog',
  standalone: true,
  templateUrl: './catalog.component.html',
  providers: [ProductService, CartService, AuthService],
  imports: [CommonModule]
})
export class Catalog implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe(data => {
      console.log('Products received:', data);
      this.products = data;
    });
  }

  addToCart(product: Product): void {
    const item: CartItem = {
      productId: product.id,
      customerId: this.authService.getCustomerId(),
      name: product.name,
      price: product.price,
      quantity: 1
    };
    var count = 0;
    for (const p of this.products) {
      if (p.id === product.id) {
        count = p.stock;
      }
    }
    var cartCount = 0;
    this.cartService.getItems().subscribe(cartItems => {
      for (const cartItem of cartItems) {
        if (cartItem.productId === product.id) {
          cartCount += cartItem.quantity;
        }
      }
      if (count > cartCount) {
        console.log('Adding to cart:', item);
        this.cartService.addItem(item).subscribe({
          next: () => alert(`${product.name} added to cart.`),
          error: err => console.error('Error adding to cart:', err)
        });
      } else { 
        alert(`Cannot add more ${product.name} to cart. Stock limit reached.`);
      }
    });
  }

  checkout(): void {
    alert('Proceeding to checkout...');
    // Implement checkout logic here
  }
}
