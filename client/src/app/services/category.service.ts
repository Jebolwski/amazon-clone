import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductCategory } from '../interfaces/product';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  allCategories: ProductCategory[] = [];

  constructor(private http: HttpClient) {
    this.getAllCategories();
  }

  public addCategory(data: { name: string; description: string }) {
    this.http
      .post(this.baseApiUrl + 'ProductCategory/add', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        console.log(res);
      });
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
}
