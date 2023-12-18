import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category';
import { BaseProduct, Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  form!: FormGroup;
  product: Product = this.createObjectProduct();
  products: Product[] = [];
  categories: Category[] = [];
  show: boolean = true;
  hasError: boolean = false;
  hasSuccess: boolean = false;
  msgError?: string;
  msgSuccess?: string;

  constructor(private formBuilder: FormBuilder,
              private productService: ProductService,
              private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getAll();
    this.formValidation();
    this.getAllCategories();
  }

  get name() {
    return this.form.get('name');
  }

  get price() {
    return this.form.get('price');
  }

  get color() {
    return this.form.get('color');
  }

  get categoryId() {
    return this.form.get('categoryId');
  }

  get description() {
    return this.form.get('description');
  }

  formValidation(): void {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required, Validators.maxLength(100)]],
      description: [null, [Validators.required, Validators.maxLength(150)]],
      price: [null, [Validators.required]],
      categoryId: [null, [Validators.required]],
      color: [null],
    });
  }

  commandSaveOrChange(): void {
    if(!this.product.id)
      this.post();
    else
      this.put();
  }

  getById(product: Product): void {
    this.product = product;
    this.showList();
  }

  getAllCategories(): void {
    this.categoryService.getAll().subscribe({
      next: value => this.categories = value,
      error: (err) => this.notificationError(err.error)
    });
  }

  getAll(): void {
    this.productService.getAll().subscribe({
      next: value => this.products = value,
      error: (err) => this.notificationError(err.error)
    });
  }

  post(): void {
    this.productService.create(this.createObjectBaseProduct()).subscribe({
      next: () =>  this.notificationSuccess('registered'),
      error: (err) => this.notificationError(err.error)
    });
  }

  put(): void {
    this.productService.update(this.product).subscribe({
      next: () => this.notificationSuccess('updated'),
      error: (err) => this.notificationError(err.error)
    });
  }

  remove(id: string): void {
    this.productService.delete(id).subscribe({
      next: () => this.notificationSuccess('delete'),
      error: (err) => this.notificationError(err.error)
    });
  }

  notificationSuccess(value: string): void {
    if(value !== 'delete')
      this.showList();

    this.getAll();
    this.msgSuccess = 'Product ' + value + ' with success';
    this.hasSuccess = true;
    this.hasError = false;

    setTimeout(() => {
      this.msgSuccess = '';
      this.hasSuccess = false;
    }, 2000);
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
    this.product = this.createObjectProduct();
    this.formValidation();
    this.showList();
  }

  showList(): boolean {
    this.show = !this.show;
    return this.show;
  }

  createObjectProduct(): Product {
    return {
      id: '',
      name: '',
      description: '',
      price: 0,
      color: '',
      categoryId: '',
      categoryName: '',
      categoryDescription: ''};
  }

  createObjectBaseProduct(): BaseProduct {
    return {
      categoryId: this.product.categoryId,
      name: this.product.name,
      description: this.product.description,
      price: this.product.price,
      color: this.product.color
    };
  }
}
