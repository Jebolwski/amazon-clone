import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { Login2Component } from './components/login2/login2.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { SearchProductsComponent } from './components/search-products/search-products.component';
import { ProductComponent } from './components/product/product.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { RegisterComponent } from './components/register/register.component';
import { AddCategoryComponent } from './components/add-category/add-category.component';
import { AllCategoriesComponent } from './components/all-categories/all-categories.component';
import { DeleteCategoryComponent } from './components/delete-category/delete-category.component';
import { UpdateProductComponent } from './components/update-product/update-product.component';
import { DeleteProductComponent } from './components/delete-product/delete-product.component';
import { UpdateCategoryComponent } from './components/update-category/update-category.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { CommentComponent } from './components/comment/comment.component';
import { CategoryDetailComponent } from './components/category-detail/category-detail.component';
import { register } from 'swiper/element/bundle';
import { DeleteCommentComponent } from './components/delete-comment/delete-comment.component';

register();
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    Login2Component,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    SearchProductsComponent,
    ProductComponent,
    AddProductComponent,
    RegisterComponent,
    AddCategoryComponent,
    AllCategoriesComponent,
    DeleteCategoryComponent,
    UpdateProductComponent,
    DeleteProductComponent,
    UpdateCategoryComponent,
    ProductDetailComponent,
    CommentComponent,
    CategoryDetailComponent,
    DeleteCommentComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
