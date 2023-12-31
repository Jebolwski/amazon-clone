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
  profile!: User;
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
        id: 'sadas',
      };
      this.http
        .post(
          this.baseApiUrl +
            'Authentication/search-by-username?name=' +
            jwtData[
              'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
            ],
          {}
        )
        .subscribe((res: any) => {
          let response: Response = res;
          if (response.statusCode == 200) {
            this.user.id = response.responseModel.id;
          } else {
            this.notyf.error(response.message);
          }
        });
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
            id: 'asas',
          };
          //! get user
          this.http
            .post(
              this.baseApiUrl +
                'Authentication/search-by-username?name=' +
                this.user.username,
              {}
            )
            .subscribe((res: any) => {
              let response: Response = res;
              if (response.statusCode === 200) {
                this.user.id = response.responseModel.id;
                this.router.navigate(['/']);
                this.notyf.success('Başarıyla giriş yapıldı. 🥰');
              } else {
                this.notyf.error(response.message);
              }
            });
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
        let response: Response = res;
        if (response.statusCode == 200) {
          this.router.navigate(['/login-2'], {
            state: { name: formData.name },
          });
        } else {
          this.notyf.error(response.message);
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

  public getUser(id: string) {
    this.http
      .get(this.baseApiUrl + 'Authentication/' + id)
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode == 200) {
          this.profile = response.responseModel;
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
