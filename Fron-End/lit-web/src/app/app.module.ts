import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponent } from './components/product/product.component';
import { CategoryComponent } from './components/category/category.component';
import { CategoryService } from './services/category.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ListCategoryComponent } from './components/category/list-category/list-category.component';
import { ListProductComponent } from './components/product/list-product/list-product.component';
import { NavMenuComponent } from './shared/nav-menu/nav-menu.component';
import { NotificationsComponent } from './shared/notifications/notifications.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    CategoryComponent,
    ListCategoryComponent,
    ListProductComponent,
    NavMenuComponent,
    NotificationsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'})
  ],
  exports: [ProductComponent, CategoryComponent,],
  providers: [CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
