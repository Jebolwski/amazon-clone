import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Notyf } from 'notyf';
import { Response } from '../interfaces/response';
import { Product } from '../interfaces/product';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  notyf = new Notyf();
  cart: { id: string; userId: string; products: Product[]; total: string } = {
    id: '',
    products: [],
    total: '0',
    userId: '',
  };
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}

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
          this.getCartsProducts(this.authService.user.id);
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
          let productsArray: Product[] = [];

          response.responseModel.products.forEach((product: Product) => {
            if (
              productsArray.filter((x: Product) => x.id == product.id).length ==
              0
            ) {
              product.count = 1;
              productsArray.push(product);
            } else {
              productsArray.filter(
                (x: Product) => x.id == product.id
              )[0].count! += 1;
            }
          });
          const f = new Intl.NumberFormat('tr-TR', {
            style: 'currency',
            currency: 'TRY',
            minimumFractionDigits: 2,
          });
          productsArray = productsArray.sort((a, b) => {
            return a.name.length - b.name.length;
          });
          this.cart = {
            id: response.responseModel.cart.id,
            products: productsArray,
            userId: response.responseModel.cart.userId,
            total: '0',
          };
          this.cart?.products?.forEach((product) => {
            this.cart.total = String(
              parseFloat(this.cart.total) +
                parseFloat(product.price) * (product.count || 1)
            );
            product.price = f.format(parseFloat(product.price));
          });
          this.cart.total = f.format(parseFloat(this.cart.total));
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  removeProductFromCart(productId: string, cartId: string) {
    this.http
      .delete(this.baseApiUrl + 'cart/Cart/' + productId + '/' + cartId, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.getCartsProducts(this.authService.user.id);
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  buyTheCart(cartId: string) {
    this.http
      .post(
        this.baseApiUrl + 'cart/Cart/' + cartId + '/buy',
        {},
        {
          headers: new HttpHeaders().append(
            'Authorization',
            `Bearer ${localStorage.getItem('accessToken')}`
          ),
        }
      )
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
      });
  }
}
