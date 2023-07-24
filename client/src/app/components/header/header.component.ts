import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(
    public auth: AuthService,
    public categoryService: CategoryService,
    public productService: ProductService,
    public router: Router
  ) {}

  public searchForm: FormGroup = new FormGroup({
    text: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(60),
    ]),
    category: new FormControl('', [Validators.required]),
  });

  get text(): string {
    return this.searchForm.get('text')?.value;
  }

  get category(): string {
    return this.searchForm.get('category')?.value;
  }

  public searchIt(name: string, category: string): void {
    if (name == '') {
      name = "''";
    }
    console.log(category == '');

    this.productService.getByNameAndCategory(name, category);
    if (category == '') {
      this.router.navigate(['/search-products/' + name + '/' + "''"]);
    } else {
      this.router.navigate(['/search-products/' + name + '/' + category]);
    }
  }

  toggleSideBar() {
    let sidebar = document.querySelector('.sidebar');
    console.log('messi');
    sidebar?.classList.toggle('-left-full');
    sidebar?.classList.toggle('left-0');
  }
}
