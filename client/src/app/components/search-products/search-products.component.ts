import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-search-products',
  templateUrl: './search-products.component.html',
  styleUrls: ['./search-products.component.scss'],
})
export class SearchProductsComponent implements OnInit {
  name!: string | null;
  category!: string | null;
  productsPerPage: number = 2;
  pageNumber: number = 1;
  totalPages: number = this.product.products.length;

  constructor(private route: ActivatedRoute, public product: ProductService) {
    console.log(this.totalPages, this.pageNumber);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      console.log(params);
      this.product.getByNameAndCategory(
        params.get('name') || '',
        params.get('category') || ''
      );
      this.name = params.get('name');
      if (this.name == "''") {
        this.name = 'empty';
      }
      this.category = params.get('category');
    });
  }
}
