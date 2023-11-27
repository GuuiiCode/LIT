import { Category } from './../../../models/category';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-list-category',
  templateUrl: './list-category.component.html',
  styleUrls: ['./list-category.component.css']
})
export class ListCategoryComponent {
  @Input() categories: Category[] = [];

  @Output() update = new EventEmitter<Category>();

  @Output() delete = new EventEmitter<string>();

  constructor() { }

  put(category: Category): void {
    this.update.emit(category);
  }

  remove(id: string): void {
    this.delete.emit(id);
  }
}
