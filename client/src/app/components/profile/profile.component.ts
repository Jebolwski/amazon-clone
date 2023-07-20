import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  id: string = '';
  constructor(public auth: AuthService, private route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    auth.getUser(this.id);
  }
}
