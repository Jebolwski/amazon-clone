import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'amazon-clone-front';

  constructor(public auth: AuthService, public router: Router) {
    if (!auth.user) {
      this.router.navigate(['/login']);
    }
  }
}
