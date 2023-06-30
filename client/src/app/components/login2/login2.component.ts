import { HttpClient, HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Navigation, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login2',
  templateUrl: './login2.component.html',
  styleUrls: ['./login2.component.scss'],
})
export class Login2Component {
  constructor(
    private http: HttpClient,
    private router: Router,
    public auth: AuthService
  ) {
    let nav: Navigation | null = this.router.getCurrentNavigation();
    if (nav != null) {
      this.name = nav.extras.state?.['name'];
    }
  }

  public toggleDetails: boolean = false;
  public name!: string;

  public toggleDetailsFunc() {
    this.toggleDetails = !this.toggleDetails;
  }

  public loginForm: FormGroup = new FormGroup({
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(40),
    ]),
  });

  get password(): string {
    return this.loginForm.get('password')?.value || '';
  }
}
