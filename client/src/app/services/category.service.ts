import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product, ProductCategory } from '../interfaces/product';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  allCategories: ProductCategory[] = [];
  category!: ProductCategory;

  constructor(private http: HttpClient) {
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
        console.log(res);
        if (res['id'] != null) {
          bool = true;
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
        this.allCategories = res;
      });
  }

  public deleteCategory(id: string): boolean {
    let result: boolean = false;
    this.http
      .delete(this.baseApiUrl + 'ProductCategory/' + id + '/delete', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        result = res;
      });
    return result;
  }

  public getCategoryById(id: string) {
    this.http
      .get(this.baseApiUrl + 'ProductCategory/' + id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        if (res['id'] != null) {
          this.category = res;
        }
      });
  }
}
