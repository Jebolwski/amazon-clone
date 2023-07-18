import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent {
  id!: string;
  constructor(public cartService: CartService, private route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    cartService.getCartsProducts(this.id);
    setTimeout(() => {});
  }
}
