import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoggedService {
  constructor(private auth: AuthService) {
    console.log(this.auth.user);
  }

  canActivate(): boolean {
    let flag: boolean = false;
    if (
      localStorage.getItem('accessToken') &&
      localStorage.getItem('refreshToken') &&
      this.auth.user
    ) {
      flag = true;
    } else {
      this.auth.logout();
    }
    return flag;
  }
}
