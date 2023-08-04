import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { map } from 'rxjs';
import { Response } from '../interfaces/response';
import { Address } from '../interfaces/address';
import { Notyf } from 'notyf';

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  private notyf: Notyf = new Notyf();
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}

  getYourAddresses() {
    return this.http
      .get(this.baseApiUrl + 'Address/address/' + this.authService.user.id, {
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
            return res.responseModel;
          } else {
            this.notyf.error(res.message);
          }
        })
      );
  }
}
