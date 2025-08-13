import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ProductService } from "../services/product.service";
import { Product } from "../models/product.class";

@Component({
    selector: 'app-catalog',
    standalone: true,
    templateUrl: './catalog.component.html',
    providers: [ProductService],
    imports: [CommonModule]
})

export class Catalog implements OnInit{

    products: Product[] = [];
    
    constructor(private productService: ProductService) {
    }

    ngOnInit(): void {
    this.productService.getProducts().subscribe(data => {

      console.log('Products received:', data); 
      this.products = data;
    });
  }
}