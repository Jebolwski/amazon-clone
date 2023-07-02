import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(public auth: AuthService) {}

  public searchForm: FormGroup = new FormGroup({
    text: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(60),
    ]),
  });

  get text(): string {
    return this.searchForm.get('text')?.value;
  }
}
