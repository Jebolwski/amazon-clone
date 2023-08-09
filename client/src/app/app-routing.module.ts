import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { Login2Component } from './components/login2/login2.component';
import { NotLoggedService } from './services/notlogged.service';
import { HomeComponent } from './components/home/home.component';
import { LoggedService } from './services/logged.service';
import { SearchProductsComponent } from './components/search-products/search-products.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { RegisterComponent } from './components/register/register.component';
import { AddCategoryComponent } from './components/add-category/add-category.component';
import { AllCategoriesComponent } from './components/all-categories/all-categories.component';
import { AdminuserService } from './services/adminuser.service';
import { DeleteCategoryComponent } from './components/delete-category/delete-category.component';
import { UpdateProductComponent } from './components/update-product/update-product.component';
import { DeleteProductComponent } from './components/delete-product/delete-product.component';
import { UpdateCategoryComponent } from './components/update-category/update-category.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { CommentComponent } from './components/comment/comment.component';
import { CategoryDetailComponent } from './components/category-detail/category-detail.component';
import { DeleteCommentComponent } from './components/delete-comment/delete-comment.component';
import { UpdateCommentComponent } from './components/update-comment/update-comment.component';
import { CartComponent } from './components/cart/cart.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AddressesComponent } from './components/addresses/addresses.component';
import { CreditCartsComponent } from './components/credit-carts/credit-carts.component';
import { AddCreditCartComponent } from './components/add-credit-cart/add-credit-cart.component';
import { AddAddressComponent } from './components/add-address/add-address.component';
import { AddressDetailComponent } from './components/address-detail/address-detail.component';
import { CreditCartDetailComponent } from './components/credit-cart-detail/credit-cart-detail.component';
import { DeleteAddressComponent } from './components/delete-address/delete-address.component';
import { DeleteCreditCartComponent } from './components/delete-credit-cart/delete-credit-cart.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'Giriş Yap',
    canActivate: [NotLoggedService],
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Kayıt Ol',
    canActivate: [NotLoggedService],
  },
  {
    path: 'login-2',
    component: Login2Component,
    title: 'Giriş Yap 2',
    canActivate: [NotLoggedService],
  },
  {
    path: '',
    component: HomeComponent,
    title: 'Ev',
    canActivate: [LoggedService],
  },
  {
    path: 'search-products/:name/:category',
    component: SearchProductsComponent,
    title: 'Ürün Ara',
    canActivate: [LoggedService],
  },
  {
    path: 'add-product',
    component: AddProductComponent,
    title: 'Ürün Ekle',
    canActivate: [AdminuserService],
  },
  {
    path: 'add-category',
    component: AddCategoryComponent,
    title: 'Ürün Kategorisi Ekle',
    canActivate: [AdminuserService],
  },
  {
    path: 'all-categories',
    component: AllCategoriesComponent,
    title: 'Bütün Ürün Kategorileri',
    canActivate: [AdminuserService],
  },
  {
    path: 'category/:id/update',
    component: UpdateCategoryComponent,
    title: 'Ürün Kategorisi Düzenle',
    canActivate: [AdminuserService],
  },
  {
    path: 'category/:id/delete',
    component: DeleteCategoryComponent,
    title: 'Ürün Kategorisi Sil',
    canActivate: [AdminuserService],
  },
  {
    path: 'product/:id/update',
    component: UpdateProductComponent,
    title: 'Ürünü Düzenle',
    canActivate: [AdminuserService],
  },
  {
    path: 'product/:id/delete',
    component: DeleteProductComponent,
    title: 'Ürünü Sil',
    canActivate: [AdminuserService],
  },
  {
    path: 'product/:id',
    component: ProductDetailComponent,
    title: 'Ürün Detayı',
  },
  {
    path: 'comment/:id',
    component: CommentComponent,
    title: 'Yorum Detayı',
    canActivate: [LoggedService],
  },
  {
    path: 'category/:id',
    component: CategoryDetailComponent,
    title: 'Kategori Detayı',
    canActivate: [AdminuserService],
  },
  {
    path: 'comment/:id/delete',
    component: DeleteCommentComponent,
    title: 'Yorum Sil',
    canActivate: [LoggedService],
  },
  {
    path: 'comment/:id/update',
    component: UpdateCommentComponent,
    title: 'Yorum Düzenle',
    canActivate: [LoggedService],
  },
  {
    path: 'cart/:id',
    component: CartComponent,
    title: 'Kart',
    canActivate: [LoggedService],
  },
  {
    path: 'profile/:id',
    component: ProfileComponent,
    title: 'Profil',
  },
  {
    path: 'addresses',
    component: AddressesComponent,
    title: 'Adresler',
  },
  {
    path: 'credit-carts',
    component: CreditCartsComponent,
    title: 'Kredi Kartları',
  },
  {
    path: 'add-credit-cart',
    component: AddCreditCartComponent,
    title: 'Kredi Kartı ekle',
  },
  {
    path: 'add-address',
    component: AddAddressComponent,
    title: 'Kredi Adres ekle',
  },
  {
    path: 'address/:id',
    component: AddressDetailComponent,
    title: 'Adres Detayı',
  },
  {
    path: 'credit-cart/:id',
    component: CreditCartDetailComponent,
    title: 'Adres Detayı',
  },
  {
    path: 'credit-cart/:id/delete',
    component: DeleteCreditCartComponent,
    title: 'Kredi kartı sil',
  },
  {
    path: 'address/:id/delete',
    component: DeleteAddressComponent,
    title: 'Adresi sil',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
