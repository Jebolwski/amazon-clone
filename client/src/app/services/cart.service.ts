import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Notyf } from 'notyf';
import { Response } from '../interfaces/response';
import { Product } from '../interfaces/product';
import { AuthService } from './auth.service';
import { map } from 'rxjs';

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

  cartStatusOne: {
    id: string;
    userId: string;
    products: Product[];
    total: string;
  } = { id: '', products: [], total: '0', userId: '' };
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}

  addToCart(data: { productId: string; count: number }) {
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
            this.cart.total =
              this.cart.total +
              parseFloat(product.price) * (product.count || 1);
            // product.price = f.format(parseFloat(product.price));
          });
          // this.cart.total = f.format(parseFloat(this.cart.total));

          this.cartStatusOne.id = this.cart.id;
          this.cartStatusOne.userId = this.cart.userId;

          this.cartStatusOne.products = this.cart.products.filter((product) => {
            return product.status == 1;
          });

          let total = 0;
          this.cartStatusOne.products.forEach((product) => {
            total += parseFloat(product.price) * (product.count || 1);
            // product.price = f.format(parseFloat(product.price));
          });
          this.cart.total = parseFloat(String(this.cart.total)).toFixed(2);
          this.cartStatusOne.total = parseFloat(String(total)).toFixed(2);
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
    this.http
      .post(
        this.baseApiUrl + 'Bought/add-bought',
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

  buyNow(cartId: string, productId: string) {
    this.http
      .post(
        this.baseApiUrl + 'cart/Cart/' + cartId + '/' + productId + '/buy-now',
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
    this.router.navigate(['/cart/' + this.authService.user.id + '/finish']);
  }

  toggleStatus(productId: string, cartId: string) {
    return this.http
      .post(
        this.baseApiUrl +
          'cart/Cart/' +
          cartId +
          '/' +
          productId +
          '/toggle-status',
        {},
        {
          headers: new HttpHeaders().append(
            'Authorization',
            `Bearer ${localStorage.getItem('accessToken')}`
          ),
        }
      )
      .pipe(
        map((res: any) => {
          let response: Response = res;
          if (response.statusCode === 200) {
            return response.responseModel;
          } else {
            return null;
          }
        })
      );
  }

  toggleAllOf(cartId: string) {
    return this.http
      .post(
        this.baseApiUrl + 'cart/Cart/' + cartId + '/toggle-off',
        {},
        {
          headers: new HttpHeaders().append(
            'Authorization',
            `Bearer ${localStorage.getItem('accessToken')}`
          ),
        }
      )
      .pipe(
        map((res: any) => {
          let response: Response = res;
          if (response.statusCode === 200) {
            return response.responseModel;
          } else {
            return null;
          }
        })
      );
  }
}
