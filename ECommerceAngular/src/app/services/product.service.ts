import { Injectable } from '@angular/core'
import { HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Product } from '../models/product.class';


@Injectable({
    providedIn: 'root'
})

export class ProductService {
    private apiUrl = 'http://localhost:5146/api/product'

    constructor(private http: HttpClient) {
    }

    getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(this.apiUrl);
    }
}