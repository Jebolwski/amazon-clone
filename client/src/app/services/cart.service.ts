import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Notyf } from 'notyf';
import { Response } from '../interfaces/response';
import { Product } from '../interfaces/product';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  notyf = new Notyf();
  cart!: { id: string; userId: string; products: Product[] };
  constructor(private http: HttpClient, private router: Router) {}

  addToCart(data: { productId: string }) {
    this.http
      .post(this.baseApiUrl + 'cart/Cart/add-to-cart', data, {
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
      });
  }

  getCartsProducts(id: string) {
    this.http
      .get(this.baseApiUrl + 'cart/Cart/cart/' + id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.cart = {
            id: response.responseModel.cart.id,
            products: response.responseModel.products,
            userId: response.responseModel.cart.userId,
          };
          this.cart?.products?.forEach((product) => {
            const f = new Intl.NumberFormat('tr-TR', {
              style: 'currency',
              currency: 'TRY',
              minimumFractionDigits: 2,
            });
            product.price = f.format(parseInt(product.price));
            console.log(product.price);
          }, 2000);
        } else {
          this.notyf.error(response.message);
        }
      });
  }
}
