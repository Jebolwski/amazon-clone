import { Component } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/interfaces/product';

@Component({
  selector: 'app-delete-product',
  templateUrl: './delete-product.component.html',
  styleUrls: ['./delete-product.component.scss'],
})
export class DeleteProductComponent {
  id: string | null;
  product!: Product;
  constructor(
    public productService: ProductService,
    private location: Location,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id);

    this.productService.getProduct(this.id!).subscribe((res: Product) => {
      this.product = res;
    });
  }

  delete() {
    let result = this.productService.deleteProduct(this.id!);
    this.back();
  }

  back(): void {
    this.location.back();
  }
}
