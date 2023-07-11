import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response } from '../interfaces/response';
import { Notyf } from 'notyf';
import { Router } from '@angular/router';
import { addProductPhoto } from '../interfaces/addProductPhoto';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(private http: HttpClient, private router: Router) {}

  private baseApiUrl: string = 'http://localhost:5044/api/';
  notyf = new Notyf();
  postComment(data: {
    title: string;
    comment: string;
    stars: number;
    commentPhotos: addProductPhoto[];
    productId: string;
  }) {
    this.http
      .post(this.baseApiUrl + 'comment/Comment/post', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.notyf.success(response.message);
          this.router.navigate(['product/' + data.productId]);
        } else {
          this.notyf.error(response.message);
        }
      });
  }
}
