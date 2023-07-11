import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product, ProductCategory } from '../interfaces/product';
import { Response } from '../interfaces/response';
import { Notyf } from 'notyf';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  allCategories: ProductCategory[] = [];
  headerAllCategories: ProductCategory[] = [];
  category!: ProductCategory;
  notyf = new Notyf();

  constructor(private http: HttpClient, private router: Router) {
    this.getAllCategories();
  }

  public addCategory(data: { name: string; description: string }): boolean {
    let bool: boolean = false;
    this.http
      .post(this.baseApiUrl + 'ProductCategory/add', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.router.navigate(['/all-categories']);
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
      });
    return bool;
  }

  public updateCategory(data: { name: string; description: string }): boolean {
    let bool: boolean = false;
    this.http
      .put(this.baseApiUrl + 'ProductCategory/update', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.router.navigate(['/all-categories']);
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
      });
    return bool;
  }

  public getAllCategories() {
    this.http
      .get(this.baseApiUrl + 'ProductCategory/get-all-categories', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        this.allCategories = response.responseModel;
        this.headerAllCategories = response.responseModel;
      });
  }

  public deleteCategory(id: string) {
    this.http
      .delete(this.baseApiUrl + 'ProductCategory/' + id + '/delete', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.router.navigate(['/all-categories']);
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  public async getCategoryById(id: string) {
    this.http
      .get(this.baseApiUrl + 'ProductCategory/' + id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.category = response.responseModel;
        } else {
          this.notyf.error(response.message);
        }
      });
  }
}
