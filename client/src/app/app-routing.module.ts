import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { Login2Component } from './components/login2/login2.component';
import { NotLoggedService } from './services/notlogged.service';
import { HomeComponent } from './components/home/home.component';
import { LoggedService } from './services/logged.service';
import { SearchProductsComponent } from './components/search-products/search-products.component';
import { AddProductComponent } from './components/add-product/add-product.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'Giriş Yap',
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
    path: 'search-products/:string',
    component: SearchProductsComponent,
    title: 'Ürün Ara',
    canActivate: [LoggedService],
  },
  {
    path: 'add-product',
    component: AddProductComponent,
    title: 'Ürün Ekle',
    canActivate: [LoggedService],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
