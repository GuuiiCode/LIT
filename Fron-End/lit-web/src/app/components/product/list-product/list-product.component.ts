import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-list-product',
  templateUrl: './list-product.component.html',
  styleUrls: ['./list-product.component.css']
})
export class ListProductComponent {
  @Input() products: Product[] = [];

  @Output() update = new EventEmitter<Product>();

  @Output() delete = new EventEmitter<string>();

  constructor() { }

  put(product: Product): void {
    this.update.emit(product);
  }

  remove(id: string): void {
    this.delete.emit(id);
  }
}
