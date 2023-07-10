import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Notyf } from 'notyf';
import { ProductCategory } from 'src/app/interfaces/product';
import { Response } from 'src/app/interfaces/response';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-update-category',
  templateUrl: './update-category.component.html',
  styleUrls: ['./update-category.component.scss'],
})
export class UpdateCategoryComponent implements OnInit {
  id!: string | null;
  updateCategoryForm!: FormGroup;
  private baseApiUrl: string = 'http://localhost:5044/api/';
  notyf = new Notyf();
  category!: ProductCategory;

  constructor(
    public categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') || '0';

    this.http
      .get(this.baseApiUrl + 'ProductCategory/' + this.id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.category = response.responseModel;
          console.log(this.category?.name);
          this.updateCategoryForm = new FormGroup({
            name: new FormControl(this.category?.name, [
              Validators.required,
              Validators.minLength(3),
              Validators.maxLength(40),
            ]),
            description: new FormControl(this.category?.description, [
              Validators.required,
              Validators.minLength(10),
              Validators.maxLength(200),
            ]),
          });
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  get name() {
    return this.updateCategoryForm.get('name');
  }

  get description() {
    return this.updateCategoryForm.get('description');
  }

  jsonData: {
    name: string;
    description: string;
  } = {
    name: '',
    description: '',
  };

  constructData(): {
    name: string;
    description: string;
  } {
    this.jsonData = {
      name: this.updateCategoryForm.get('name')?.value,
      description: this.updateCategoryForm.get('description')?.value,
    };
    return this.jsonData;
  }

  updateIt() {
    let jsonData = this.constructData();
    let res = this.categoryService.updateCategory(jsonData);
    if (res == true) {
      this.router.navigate(['/']);
    }
  }
}
