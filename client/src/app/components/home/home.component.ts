import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Notyf } from 'notyf';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(public auth: AuthService) {}
}
