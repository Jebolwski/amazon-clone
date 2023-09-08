import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Notyf } from 'notyf';
import { map } from 'rxjs';
import { Response } from '../interfaces/response';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class BoughtService {
  private baseApiUrl: string = 'http://localhost:5044/api/';

  constructor(
    private http: HttpClient,
    private router: Router,
    private auth: AuthService,
    private location: Location
  ) {}

  notyf = new Notyf();

  getAllBoughts() {
    return this.http
      .get(this.baseApiUrl + 'Bought/all-bought', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (response.statusCode === 200) {
            return response.responseModel;
          } else {
            return null;
          }
        })
      );
  }

  getAllArchivedBoughts() {
    return this.http
      .get(this.baseApiUrl + 'Bought/all-bought-archived', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (response.statusCode === 200) {
            return response.responseModel;
          } else {
            return null;
          }
        })
      );
  }

  deleteBoughts(id: string) {
    return this.http
      .delete(this.baseApiUrl + 'Bought/delete-bought/' + id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (response.statusCode === 200) {
            this.notyf.success(response.message);
            return response.responseModel;
          } else {
            return null;
          }
        })
      );
  }

  toggleBought(id: string) {
    return this.http
      .post(
        this.baseApiUrl + 'Bought/toggle-boughts/' + id,
        {},
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
          if (response.statusCode === 200) {
            this.notyf.success(response.message);
            return { mesi: 'ronaldo' };
          } else {
            return null;
          }
        })
      );
  }
}
