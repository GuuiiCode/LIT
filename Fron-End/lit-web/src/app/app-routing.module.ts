import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './components/product/product.component';
import { CategoryComponent } from './components/category/category.component';

const routes: Routes = [
  {
    path: 'product-list',
    component: ProductComponent,
    title: 'Product Page'
  },
  {
    path: 'category-list',
    component: CategoryComponent,
    title: 'Category Page'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
