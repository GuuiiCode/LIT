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
  showList: boolean = true;
  isUpdate: boolean = false;
  isSave: boolean = true;

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

  getAllCategories(): void {
    this.categoryService.getAll().subscribe((data:Category[]) => {
      this.categories = data;
    }, (error) => {
        console.log(error);
        alert('Internal system error');
    });
  }

  getAll(): void {
    this.productService.getAll().subscribe((data:Product[]) => {
      this.products = data;
    }, (error) => {
        console.log(error);
        alert('Internal system error');
    })
  }

  getById(product: Product): void {
    this.product = product;
    this.showList = false;
  }

  post(): void {
    this.productService.create(this.createObjectBaseProduct()).subscribe(data => {
      if (data) {
        alert('Product register with success');
        this.getAll();
        this.showList = true;
      } else {
        alert('Error registering product');
      }
    }, (error) => {
      console.log(error);
      alert('Internal system error');
    })
  }

  put(): void {
    this.productService.update(this.product).subscribe(() => {
      alert('Product update with success');
      this.showList = true;
      this.isUpdate = true;
      this.isSave = false;
    }, (error) => {
      console.log(error);
      alert('Internal system error');
    });
  }

  remove(id: string): void {
    this.productService.delete(id).subscribe(() => {
      alert('Product delete with success');
      this.getAll();
    }, (error) => {
      console.log(error);
      alert('Internal system error');
    })
  }

  clearFields(): void {
    this.showList = !this.showList;
    this.product = this.createObjectProduct();
    this.formValidation();
  }

  createObjectProduct(): Product {
    return {id: '', categoryId: '', name: '', description: '', price: 0, color: ''};
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
