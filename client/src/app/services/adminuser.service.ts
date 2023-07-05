import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AdminuserService {
  constructor(private auth: AuthService) {}

  canActivate(): boolean {
    let flag: boolean = false;
    if (
      localStorage.getItem('accessToken') &&
      localStorage.getItem('refreshToken') &&
      this.auth.user &&
      this.auth.user['role'] == 'Admin'
    ) {
      flag = true;
    }
    return flag;
  }
}
