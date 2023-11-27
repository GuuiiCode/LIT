import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseCategory, Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  form!: FormGroup;
  category: Category = this.createObjectCategory();
  categories: Category[] = [];
  show: boolean = false;

  constructor(private formBuilder: FormBuilder,
              private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getAll();
    this.formValidation();
  }

  get name() {
    return this.form.get('name');
  }

  get description() {
    return this.form.get('description');
  }

  formValidation(): void {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required, Validators.maxLength(100)]],
      description: [null, [Validators.required, Validators.maxLength(150)]],
    });
  }

  commandSaveOrChange(): void {
    if(!this.category.id)
      this.post();
    else
      this.put();
  }

  getAll(): void {
    this.categoryService.getAll().subscribe({
      next: value => this.categories = value,
      error: err => this.notificationError('fetch all data', err)
    });
  }

  post(): void {
    this.categoryService.create(this.createObjectBaseCategory()).subscribe({
      next: () =>  this.notificationSuccess('register'),
      error: err => this.notificationError('registering ', err)
    });
  }

  put(): void {
    this.categoryService.update(this.category).subscribe({
      next: () => this.notificationSuccess('updated'),
      error: err => this.notificationError('updating', err)
    });
  }

  remove(id: string): void {
    this.categoryService.delete(id).subscribe({
      next: () => this.notificationSuccess('delete'),
      error: err => this.notificationError('deleting', err)
    });
  }

  getById(category: Category): void {
    this.category = category;
    this.showList();
  }

  notificationSuccess(value: string): void {
    alert('Category ' + value + ' with success');
    if(value !== 'delete')
      this.showList();

    this.getAll();
  }

  notificationError(value: string, err: any): void {
    alert('Error '+ value + ' category');
    console.log('Internal system error ' + err)
  }

  clearFields(): void {
    this.category = this.createObjectCategory();
    this.formValidation();
    this.showList();
  }

  showList(): boolean {
    this.show = !this.show;
    return this.show;
  }

  createObjectCategory(): Category {
    return {
      id: '',
      name: '',
      description: '',
    };
  }

  createObjectBaseCategory(): BaseCategory {
    return {
      name: this.category.name,
      description: this.category.description,
    };
  }
}
