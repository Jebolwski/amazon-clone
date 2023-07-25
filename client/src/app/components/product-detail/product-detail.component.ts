import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { Comment, Product } from 'src/app/interfaces/product';
import { Response } from 'src/app/interfaces/response';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit {
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
    public authService: AuthService,
    public cartService: CartService
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
  }

  ngOnInit(): void {
    const f = new Intl.NumberFormat('tr-TR', {
      style: 'currency',
      currency: 'TRY',
      minimumFractionDigits: 2,
    });
    this.productService.getProduct(this.id).subscribe((res: any) => {
      this.product = res;
      this.product!.price = f.format(parseFloat(this.product?.price || ''));
      this.makeStats();
      this.averageStars();
    });
  }

  makeStats() {
    if (this.product?.comments.length == 0) {
      this.stats.fives.percentage = '0';
      this.stats.fours.percentage = '0';
      this.stats.threes.percentage = '0';
      this.stats.twos.percentage = '0';
      this.stats.ones.percentage = '0';
    } else {
      this.stats.fives = {
        count:
          this.product?.comments.filter((comment) => comment.stars == 5)
            .length || 0,
        percentage: (
          ((this.product?.comments.filter((comment) => comment.stars == 5)
            .length || 0) /
            (this.product?.comments.length || 0)) *
          100
        ).toFixed(2),
      };
      this.stats.fours = {
        count:
          this.product?.comments.filter((comment) => comment.stars == 4)
            .length || 0,
        percentage: (
          ((this.product?.comments.filter((comment) => comment.stars == 4)
            .length || 0) /
            (this.product?.comments.length || 0)) *
          100
        ).toFixed(2),
      };
      this.stats.threes = {
        count:
          this.product?.comments.filter((comment) => comment.stars == 3)
            .length || 0,
        percentage: (
          ((this.product?.comments.filter((comment) => comment.stars == 3)
            .length || 0) /
            (this.product?.comments.length || 0)) *
          100
        ).toFixed(2),
      };
      this.stats.twos = {
        count:
          this.product?.comments.filter((comment) => comment.stars == 2)
            .length || 0,
        percentage: (
          ((this.product?.comments.filter((comment) => comment.stars == 2)
            .length || 0) /
            (this.product?.comments.length || 0)) *
          100
        ).toFixed(2),
      };
      this.stats.ones = {
        count:
          this.product?.comments.filter((comment) => comment.stars == 1)
            .length || 0,
        percentage: (
          ((this.product?.comments.filter((comment) => comment.stars == 1)
            .length || 0) /
            (this.product?.comments.length || 0)) *
          100
        ).toFixed(2),
      };
    }

    let ones: any = document.querySelector('.ones');
    ones!.style.width = this.stats.ones.percentage + '%';
    if (this.stats.ones.percentage != '100') {
      ones.classList.add('rounded-l-md');
    } else {
      ones.classList.add('rounded-md');
    }
    let twos: any = document.querySelector('.twos');
    twos!.style.width = this.stats.twos.percentage + '%';
    if (this.stats.twos.percentage != '100') {
      twos.classList.add('rounded-l-md');
    } else {
      twos.classList.add('rounded-md');
    }
    let threes: any = document.querySelector('.threes');
    threes!.style.width = this.stats.threes.percentage + '%';
    if (this.stats.threes.percentage != '100') {
      threes.classList.add('rounded-l-md');
    } else {
      threes.classList.add('rounded-md');
    }
    let fours: any = document.querySelector('.fours');
    fours!.style.width = this.stats.fours.percentage + '%';
    if (this.stats.fours.percentage != '100') {
      fours.classList.add('rounded-l-md');
    } else {
      fours.classList.add('rounded-md');
    }
    let fives: any = document.querySelector('.fives');
    fives!.style.width = this.stats.fives.percentage + '%';
    if (this.stats.fives.percentage != '100') {
      fives.classList.add('rounded-l-md');
    } else {
      fives.classList.add('rounded-md');
    }
  }

  averageStars() {
    this.product?.comments.forEach((element: Comment) => {
      this.total += element.stars;
    });
    if (this.product?.comments.length == 0) {
      this.average = '0';
    }
    this.average = (this.total / (this.product?.comments?.length || 1)).toFixed(
      2
    );

    let average: any = document.querySelector('.average');
    average.style.width = parseFloat(this.average) * 20 + '%';
  }
}
