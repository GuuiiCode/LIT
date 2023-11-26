import { Category } from './../../../models/category';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-list-category',
  templateUrl: './list-category.component.html',
  styleUrls: ['./list-category.component.css']
})
export class ListCategoryComponent implements OnInit {
  @Input() categories: Category[] = [];

  @Output() update = new EventEmitter<Category>();

  @Output() delete = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
    console.log(this.categories);
  }

  put(category: Category): void {
    this.update.emit(category);
  }

  remove(id: string): void {
    this.delete.emit(id);
  }
}
