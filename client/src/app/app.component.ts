import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Response } from './interfaces/response';
import * as jwt_decode from 'jwt-decode';
import { Notyf } from 'notyf';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'amazon-clone-front';
  private baseApiUrl: string = 'http://localhost:5044/api/';
  notyf = new Notyf();

  constructor(
    public auth: AuthService,
    public router: Router,
    private http: HttpClient
  ) {
    if (!auth.user) {
      this.router.navigate(['/login']);
    }
  }

  updateToken() {
    this.http
      .post(this.baseApiUrl + 'Authentication/refresh-token', {
        reftoken: localStorage.getItem('refreshToken'),
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
          this.auth.user = {
            username:
              jwtData[
                'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
              ],
            role: jwtData[
              'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
            ],
          };
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  ngOnInit(): void {
    setInterval(() => {
      this.updateToken();
    }, 240000);
  }
}
