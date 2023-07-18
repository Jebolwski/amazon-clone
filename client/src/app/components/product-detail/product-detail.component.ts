import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { Comment, Product } from 'src/app/interfaces/product';
import { Response } from 'src/app/interfaces/response';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent {
  total: number = 0;
  private baseApiUrl: string = 'http://localhost:5044/api/';
  average: string = '0';
  product!: Product | undefined;
  id!: string;
  notyf = new Notyf();
  stats: {
    fives: { count: number; percentage: string };
    fours: { count: number; percentage: string };
    threes: { count: number; percentage: string };
    twos: { count: number; percentage: string };
    ones: { count: number; percentage: string };
  } = {
    fives: { count: 0, percentage: '' },
    fours: { count: 0, percentage: '' },
    threes: { count: 0, percentage: '' },
    twos: { count: 0, percentage: '' },
    ones: { count: 0, percentage: '' },
  };
  constructor(
    public productService: ProductService,
    private route: ActivatedRoute,
    private http: HttpClient,
    public cartService: CartService
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    // this.getProduct(this.id).then(() => {
    //   this.makeStats();
    //   this.averageStars();
    // });
    this.http
      .get(this.baseApiUrl + 'Product/' + this.id)
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.product = response.responseModel;
          this.makeStats();
          this.averageStars();
        } else {
          this.notyf.error(response.message);
        }
      });
  }

  makeStats() {
    this.stats.fives = {
      count:
        this.product?.comments.filter((comment) => comment.stars == 5).length ||
        0,
      percentage: (
        ((this.product?.comments.filter((comment) => comment.stars == 5)
          .length || 0) /
          (this.product?.comments.length || 0)) *
        100
      ).toFixed(2),
    };
    this.stats.fours = {
      count:
        this.product?.comments.filter((comment) => comment.stars == 4).length ||
        0,
      percentage: (
        ((this.product?.comments.filter((comment) => comment.stars == 4)
          .length || 0) /
          (this.product?.comments.length || 0)) *
        100
      ).toFixed(2),
    };
    this.stats.threes = {
      count:
        this.product?.comments.filter((comment) => comment.stars == 3).length ||
        0,
      percentage: (
        ((this.product?.comments.filter((comment) => comment.stars == 3)
          .length || 0) /
          (this.product?.comments.length || 0)) *
        100
      ).toFixed(2),
    };
    this.stats.twos = {
      count:
        this.product?.comments.filter((comment) => comment.stars == 2).length ||
        0,
      percentage: (
        ((this.product?.comments.filter((comment) => comment.stars == 2)
          .length || 0) /
          (this.product?.comments.length || 0)) *
        100
      ).toFixed(2),
    };
    this.stats.ones = {
      count:
        this.product?.comments.filter((comment) => comment.stars == 1).length ||
        0,
      percentage: (
        ((this.product?.comments.filter((comment) => comment.stars == 1)
          .length || 0) /
          (this.product?.comments.length || 0)) *
        100
      ).toFixed(2),
    };
    console.log(this.stats);
  }

  averageStars() {
    this.product?.comments.forEach((element: Comment) => {
      this.total += element.stars;
    });
    console.log(this.total / (this.product?.comments?.length || 1));

    this.average = (this.total / (this.product?.comments?.length || 1)).toFixed(
      2
    );
  }
}
