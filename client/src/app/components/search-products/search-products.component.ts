import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Notyf } from 'notyf';
import { Product } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-search-products',
  templateUrl: './search-products.component.html',
  styleUrls: ['./search-products.component.scss'],
})
export class SearchProductsComponent implements OnInit {
  name!: string | null;
  category!: string | null;
  products: Product[] = [];
  notyf = new Notyf();

  productsTemp: Product[] = [];
  min: number = 0;
  max: number = 1000000000;
  constructor(
    private route: ActivatedRoute,
    public productService: ProductService
  ) {
    console.log('git bakam');
  }

  f = new Intl.NumberFormat('tr-TR', {
    style: 'currency',
    currency: 'TRY',
    minimumFractionDigits: 2,
  });

  ngOnInit(): any {
    this.category = this.route.snapshot.paramMap.get('category');
    if (this.category == 'all') {
      this.category = 'all';
    }
    this.name = this.route.snapshot.paramMap.get('name');
    if (this.name == "''") {
      this.name = '+';
    }
    this.productService
      .getByNameAndCategory(this.name || '', this.category || '')
      .subscribe((res: any) => {
        this.products = res;
        this.productsTemp = res;
      });
  }

  checkIt(event: Event, num1: number, num2: number) {
    let list = (
      event.target as HTMLElement
    ).parentNode?.parentNode?.querySelectorAll('.checkbox');
    list?.forEach((element: any) => {
      element.checked = false;
    });
    let thisNode: any = event.target;
    thisNode.checked = true;
    this.min = num1;
    this.max = num2;
    this.productsTemp = [
      ...this.products.filter((p) => {
        return (
          parseFloat(p.price) >= this.min && parseFloat(p.price) <= this.max
        );
      }),
    ];
  }

  unCheck() {
    let list = document.querySelectorAll('.checkbox');
    list?.forEach((element: any) => {
      element.checked = false;
    });
    this.productsTemp = [...this.products];
    this.min = 0;
    this.max = 100000000000;
  }

  filterbyprice() {
    let low: any = (document.querySelector('.low') as HTMLInputElement).value;
    let high: any = (document.querySelector('.high') as HTMLInputElement).value;
    if (low == '' || high == '') {
      this.notyf.error('2 değeri de giriniz. 😶');
      return;
    }

    if (parseFloat(low) >= parseFloat(high)) {
      this.notyf.error('Yanlış fiyat girildi. 😞');
    } else {
      this.productsTemp = [
        ...this.products.filter((p) => {
          return (
            parseFloat(p.price) >= parseFloat(low) &&
            parseFloat(p.price) <= parseFloat(high)
          );
        }),
      ];
    }
  }
}
