import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Notyf } from 'notyf';
import { CategoryService } from 'src/app/services/category.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(
    public auth: AuthService,
    private categoryService: CategoryService
  ) {
    this.categoryService.getAllCategories();
  }
}
