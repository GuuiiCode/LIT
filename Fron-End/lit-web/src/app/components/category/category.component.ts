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
  show: boolean = true;
  hasError: boolean = false;
  hasSuccess: boolean = false;
  msgError?: string;
  msgSuccess?: string;

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
      error: (err) => this.notificationError(err.error)
    });
  }

  post(): void {
    this.categoryService.create(this.createObjectBaseCategory()).subscribe({
      next: () =>  this.notificationSuccess('registered'),
      error: (err) => this.notificationError(err.error)
    });
  }

  put(): void {
    this.categoryService.update(this.category).subscribe({
      next: () => this.notificationSuccess('updated'),
      error: (err) => this.notificationError(err.error)
    });
  }

  remove(id: string): void {
    this.categoryService.delete(id).subscribe({
      next: () => this.notificationSuccess('delete'),
      error: (err) => this.notificationError(err.error)
    });
  }

  getById(category: Category): void {
    this.category = category;
    this.showList();
  }

  notificationSuccess(value: string): void {
    if(value !== 'delete')
      this.showList();

    this.getAll();

    this.msgSuccess = 'Category ' + value + ' with success';
    this.hasSuccess = true
    setTimeout(() => {
      this.msgSuccess = '';
      this.hasSuccess = false;
    }, 2000);
    this.hasError = false;
  }

  notificationError(err?: string): void {
    if(err) {
      this.msgError = err;
      this.hasError = true;
      setTimeout(() => {
        this.msgError = '';
        this.hasError = false;
      }, 2000);
    }
    this.hasSuccess = false
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
