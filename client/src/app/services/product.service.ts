import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Product, ProductCategory } from '../interfaces/product';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  products: Product[] = [];
  productCategories: ProductCategory[] = [];
  constructor(
    private http: HttpClient,
    private router: Router,
    private auth: AuthService
  ) {}

  public getProductsByName(name: string) {
    this.http
      .get(this.baseApiUrl + 'Product/filter-by-name?productName=' + name)
      .subscribe((res: any) => {
        if (res != null) {
          this.products = res;
        }
      });
  }

  public getAllProductCategories(): void {
    this.http
      .get(this.baseApiUrl + 'ProductCategory/get-all-categories', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        this.productCategories = res;
      });
  }
}
