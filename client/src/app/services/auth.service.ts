import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode';
import { User } from '../interfaces/user';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public user!: User;
  private baseApiUrl: string = 'http://localhost:5044/api/';

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
        if (res.accessToken) {
          localStorage.setItem('accessToken', res.accessToken);
          localStorage.setItem('refreshToken', res.refreshToken.Token);
          let jwtData: any = jwt_decode.default(res.accessToken);
          this.user = {
            username:
              jwtData[
                'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
              ],
            role: jwtData[
              'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
            ],
          };
          this.router.navigate(['/']);
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
        console.log(res);
      });
  }

  public logout() {
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
  }
}
