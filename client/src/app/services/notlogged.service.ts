import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class NotLoggedService {
  constructor(private auth: AuthService) {}

  canActivate(): boolean {
    console.log('gel');

    let flag: boolean = false;
    if (!(localStorage.getItem('accessToken') && this.auth.user != undefined)) {
      flag = true;
    }
    console.log(flag);

    return flag;
  }
}
