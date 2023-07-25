import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
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
  constructor(
    private route: ActivatedRoute,
    public productService: ProductService
  ) {}
  ngOnInit(): any {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.category = params.get('category');
      if (this.category == 'all') {
        this.category = 'all';
      }
      this.name = params.get('name');
      if (this.name == "''") {
        this.name = 'empty';
      }
    });
    console.log(this.name, this.category);
    this.productService
      .getByNameAndCategory(this.name!, this.category!)
      .subscribe((res: any) => {
        this.products = res;
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
    console.log(num1, num2);
  }
}
