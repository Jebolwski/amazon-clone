import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response } from '../interfaces/response';
import { Notyf } from 'notyf';
import { Router } from '@angular/router';
import { addProductPhoto } from '../interfaces/addProductPhoto';
import { Comment } from '../interfaces/product';
import { Location } from '@angular/common';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(
    private http: HttpClient,
    private router: Router,
    private location: Location
  ) {}
  comment!: Comment;
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

  getComment(id: string) {
    this.http
      .get(this.baseApiUrl + 'comment/Comment/' + id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.comment = response.responseModel;
        }
      });
  }

  deleteComment(id: string) {
    this.http
      .delete(this.baseApiUrl + 'comment/Comment/' + id + '/delete', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
        this.location.back();
      });
  }

  updateComment(data: {
    id: string;
    comment: string;
    title: string;
    stars: number;
    commentPhotos: { photoUrl: string }[];
  }) {
    this.http
      .put(this.baseApiUrl + 'comment/Comment/update', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.notyf.success(response.message);
        } else {
          this.notyf.error(response.message);
        }
        this.location.back();
      });
  }

  getUsersComments(): any {
    return this.http
      .get(this.baseApiUrl + 'comment/Comment/get-users-comments', {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .pipe(
        map((res: any) => {
          let response: Response = res;
          if (response.statusCode === 200) {
            return response.responseModel;
          } else {
            this.notyf.error(response.message);
          }
          this.location.back();
        })
      );
  }
}
