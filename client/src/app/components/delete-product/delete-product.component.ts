import { Component } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-delete-product',
  templateUrl: './delete-product.component.html',
  styleUrls: ['./delete-product.component.scss'],
})
export class DeleteProductComponent {
  id: string | null;
  constructor(
    public productService: ProductService,
    private location: Location,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(this.id || '');
  }

  delete() {
    let result = this.productService.deleteProduct(this.id || '');
    this.back();
  }

  back(): void {
    this.location.back();
  }
}
