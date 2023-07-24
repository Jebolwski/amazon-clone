import { Component, Input } from '@angular/core';
import { Product } from 'src/app/interfaces/product';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent {
  constructor(public auth: AuthService) {
    console.log(this.product);
  }
  @Input() product!: Product;
}
