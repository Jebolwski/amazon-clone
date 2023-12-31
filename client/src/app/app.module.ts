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
import { UpdateCommentComponent } from './components/update-comment/update-comment.component';
import { CartComponent } from './components/cart/cart.component';
import { ProfileComponent } from './components/profile/profile.component';
import { CreditCartsComponent } from './components/credit-carts/credit-carts.component';
import { AddressesComponent } from './components/addresses/addresses.component';
import { AddAddressComponent } from './components/add-address/add-address.component';
import { AddCreditCartComponent } from './components/add-credit-cart/add-credit-cart.component';
import { CreditCartDetailComponent } from './components/credit-cart-detail/credit-cart-detail.component';
import { AddressDetailComponent } from './components/address-detail/address-detail.component';
import { DeleteAddressComponent } from './components/delete-address/delete-address.component';
import { DeleteCreditCartComponent } from './components/delete-credit-cart/delete-credit-cart.component';
import { UpdateAddressComponent } from './components/update-address/update-address.component';
import { UpdateCreditCartComponent } from './components/update-credit-cart/update-credit-cart.component';
import { FinishBuyingCartComponent } from './components/finish-buying-cart/finish-buying-cart.component';
import { SuccessfullyBoughtComponent } from './components/successfully-bought/successfully-bought.component';
import { BoughtsComponent } from './components/boughts/boughts.component';
import { RefundComponent } from './components/refund/refund.component';

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
    UpdateCommentComponent,
    CartComponent,
    ProfileComponent,
    CreditCartsComponent,
    AddressesComponent,
    AddAddressComponent,
    AddCreditCartComponent,
    CreditCartDetailComponent,
    AddressDetailComponent,
    DeleteAddressComponent,
    DeleteCreditCartComponent,
    UpdateAddressComponent,
    UpdateCreditCartComponent,
    FinishBuyingCartComponent,
    SuccessfullyBoughtComponent,
    BoughtsComponent,
    RefundComponent,
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
