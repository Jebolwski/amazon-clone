import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Notyf } from 'notyf';
import { AuthService } from './auth.service';
import { map } from 'rxjs';
import { Response } from '../interfaces/response';

@Injectable({
  providedIn: 'root',
})
export class CreditCartService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  private notyf: Notyf = new Notyf();
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}

  getYourCreditCarts() {
    return this.http
      .get(
        this.baseApiUrl + 'CreditCart/creditCart/' + this.authService.user.id,
        {
          headers: new HttpHeaders().append(
            'Authorization',
            `Bearer ${localStorage.getItem('accessToken')}`
          ),
        }
      )
      .pipe(
        map((response: any) => {
          let res: Response = response;
          console.log(res);
          if (res.statusCode === 200) {
            return res.responseModel;
          } else {
            this.notyf.error(res.message);
          }
        })
      );
  }

  addCreditCart(data: any) {
    return this.http
      .post(this.baseApiUrl + 'CreditCart/creditCart/add', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .pipe(
        map((response: any) => {
          let res: Response = response;
          console.log(res);
          if (res.statusCode === 200) {
            this.notyf.success(res.message);
            this.router.navigate(['/credit-carts']);
            return res.responseModel;
          } else {
            this.notyf.error(res.message);
          }
        })
      );
  }
}