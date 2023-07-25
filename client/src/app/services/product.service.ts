import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Product, ProductCategory } from '../interfaces/product';
import { AuthService } from './auth.service';
import { Response } from '../interfaces/response';
import { Notyf } from 'notyf';
import { async, map } from 'rxjs';
import { UpdateProduct } from '../interfaces/updateProduct';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  products: Product[] = [];
  product!: Product | undefined;
  productCategories: ProductCategory[] = [];
  constructor(
    private http: HttpClient,
    private router: Router,
    private auth: AuthService,
    private location: Location
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

  public async getAllProductCategories(): Promise<void> {
    this.http
      .get(this.baseApiUrl + 'ProductCategory/get-all-categories', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe(async (res: any) => {
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

  public getProduct(id: string): any {
    return this.http.get(this.baseApiUrl + 'Product/' + id).pipe(
      map((response: any) => {
        let res: Response = response;
        if (response.statusCode === 200) {
          return response.responseModel;
        } else {
          this.notyf.error(response.message);
          return null;
        }
      })
    );
  }

  public updateProduct(body: UpdateProduct): void {
    this.http
      .put(this.baseApiUrl + 'Product/update', body, {
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
        this.location.back();
      });
  }

  public deleteProduct(id: string): void {
    this.http
      .delete(this.baseApiUrl + 'Product/' + id, {
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
        console.log(res);
      });
  }

  public getByNameAndCategory(name: string, category: string): any {
    if (name == "''") {
      name = '+';
    }
    if (category == '') {
      category = '+';
    }
    this.http
      .get(
        this.baseApiUrl +
          'Product/filter-by-name-and-category/' +
          name +
          '/' +
          category
      )
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (response.statusCode === 200) {
            let products: Product[] = response.responseModel;
            const f = new Intl.NumberFormat('tr-TR', {
              style: 'currency',
              currency: 'TRY',
              minimumFractionDigits: 2,
            });
            products.forEach((product) => {
              product.price = f.format(parseFloat(product.price));
            });
            return products;
          } else {
            this.notyf.error(response.message);
            return null;
          }
        })
      );
  }
}
