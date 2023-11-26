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
  category: Category = {id: '', name: '', description: ''};
  categories: Category[] = [];
  showList: boolean = true;

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

  getAll() {
    this.categoryService.getAll().subscribe((data:Category[]) => {
      this.categories = data;
      this.showList = true;
    }, (error: any) => {
        console.log(error);
        alert('erro interno do sistema');
    })
  }

  getById(category: Category) {
    this.category = category;
    this.showList = false;
  }

  post(): void {
    let baseCategory: BaseCategory = {
      name: this.category.name,
      description: this.category.description
    };

    this.categoryService.create(baseCategory).subscribe(data => {
      if (data) {
        alert('Category register with success');
      this.showList = true;
      } else {
        alert('Error registering category');
      }
    }, (error) => {
      console.log(error);
      alert('Internal system error');
    })
  }

  put(): void {
    this.categoryService.update(this.category).subscribe(() => {
      alert('Category update with success');
      this.showList = true;
    }, (error) => {
      console.log(error);
      alert('Internal system error');
    });
  }

  remove(id: string){
    this.categoryService.delete(id).subscribe(data => {
      alert('Category delete with success');
      this.getAll();
    }, error => {
      alert('Internal system error');
    })
  }

  clearFields(): void {
    this.showList = !this.showList;
    this.category = {id: '', name: '', description: ''};
    this.formValidation();
  }
}
