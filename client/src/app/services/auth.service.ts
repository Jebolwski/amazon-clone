import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode';
import { User } from '../interfaces/user';
import { Response } from '../interfaces/response';
import { Notyf } from 'notyf';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public user!: User;
  private baseApiUrl: string = 'http://localhost:5044/api/';
  notyf = new Notyf();
  constructor(private http: HttpClient, private router: Router) {
    if (
      localStorage.getItem('accessToken') &&
      localStorage.getItem('refreshToken')
    ) {
      let jwtData: any = jwt_decode.default(
        localStorage.getItem('accessToken') || '0'
      );
      this.user = {
        username:
          jwtData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        role: jwtData[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ],
      };
    }
  }

  public login(data: { name: string; password: string }) {
    this.http
      .post(this.baseApiUrl + 'Authentication/login', {
        username: data.name,
        password: data.password,
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          localStorage.setItem(
            'accessToken',
            response.responseModel.accessToken
          );

          localStorage.setItem(
            'refreshToken',
            response.responseModel.refreshToken.token
          );
          let jwtData: any = jwt_decode.default(
            response.responseModel.accessToken
          );
          this.user = {
            username:
              jwtData[
                'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
              ],
            role: jwtData[
              'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
            ],
          };
          this.notyf.success(response.message);
          this.router.navigate(['/']);
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  public checkIfUserExists(formData: { name: string }) {
    this.http
      .post(
        this.baseApiUrl +
          'Authentication/search-by-username?name=' +
          formData.name,
        {}
      )
      .subscribe((res: any) => {
        if (res != null) {
          this.router.navigate(['/login-2'], {
            state: { name: formData.name },
          });
        }
      });
  }

  public register(data: {
    username: string;
    password: string;
    password1: string;
  }) {
    this.http
      .post(this.baseApiUrl + 'Authentication/register', {
        username: data.username,
        password: data.password,
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode == 200) {
          this.notyf.success(response.message);
          this.router.navigate(['/login']);
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  public logout() {
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
  }
}
