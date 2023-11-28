import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseProduct, Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  API: string = 'http://localhost:3000/api/Product'

  constructor(private http: HttpClient) { }

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.API);
  }

  getById(id: string): Observable<Product> {
    const url = `${this.API}/${id}`;
    return this.http.get<Product>(url);
  }

  create(product: BaseProduct): Observable<BaseProduct> {
    return this.http.post<BaseProduct>(this.API, product);
  }

  update(product: Product): Observable<Product> {
    const url =  `${this.API}/${product.id}`;
    return this.http.put<Product>(url, product);
  }

  delete(id: string): Observable<string> {
    const url =  `${this.API}/${id}`;
    return this.http.delete<string>(url);
  }
}
