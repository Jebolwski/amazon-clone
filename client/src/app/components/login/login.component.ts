import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  public toggleHelp: boolean = false;
  private baseApiUrl: string = 'http://localhost:5044/api/';

  public loginForm: FormGroup = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
  });

  public toggleHelpFunc() {
    this.toggleHelp = !this.toggleHelp;
  }

  constructor(
    private http: HttpClient,
    private router: Router,
    public auth: AuthService
  ) {}

  get name() {
    return this.loginForm.get('name');
  }
}
