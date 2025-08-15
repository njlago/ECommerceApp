import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ProductService } from "../services/product.service";
import { CartService } from "../services/cart.service";
import { Product } from "../models/product.class";
import { CartItem } from "../models/cart-item";

@Component({
  selector: 'app-catalog',
  standalone: true,
  templateUrl: './catalog.component.html',
  providers: [ProductService],
  imports: [CommonModule]
})
export class Catalog implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private cartService: CartService
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
      name: product.name,
      price: product.price,
      quantity: 1
    };

    this.cartService.addItem(item).subscribe({
      next: () => alert(`${product.name} added to cart.`),
      error: err => console.error('Error adding to cart:', err)
    });
  }
}
