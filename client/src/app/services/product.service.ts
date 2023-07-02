import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../interfaces/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  products: Product[] = [];
  constructor(private http: HttpClient, private router: Router) {}

  public getProductsByName(name: string) {
    this.http
      .get(this.baseApiUrl + 'Product/filter-by-name?productName=' + name)
      .subscribe((res: any) => {
        if (res != null) {
          this.products = res;
        }
      });
  }
}
