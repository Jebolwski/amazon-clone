import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Product, ProductCategory } from '../interfaces/product';
import { AuthService } from './auth.service';
import { Response } from '../interfaces/response';
import { Notyf } from 'notyf';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  products: Product[] = [];
  product!: Product;
  productCategories: ProductCategory[] = [];
  constructor(
    private http: HttpClient,
    private router: Router,
    private auth: AuthService
  ) {}
  notyf = new Notyf();

  public getProductsByName(name: string) {
    this.http
      .get(this.baseApiUrl + 'Product/filter-by-name/' + name)
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.products = response.responseModel;
        } else {
          this.notyf.error(
            'Aradığınız ürünler bulunurken bir sorunla karşılaşıldı. 🤨'
          );
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
        let response: Response = res;
        if (response.statusCode === 200) {
          this.productCategories = response.responseModel;
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  public addProduct(data: any): void {
    this.http
      .post(this.baseApiUrl + 'Product/add', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
        this.router.navigate(['/']);
      });
  }

  public getProduct(id: string): void {
    this.http.get(this.baseApiUrl + 'Product/' + id).subscribe((res: any) => {
      let response: Response = res;
      if (response.statusCode === 200) {
        this.product = response.responseModel;
      } else {
        this.notyf.error(response.message);
      }
    });
  }
}
