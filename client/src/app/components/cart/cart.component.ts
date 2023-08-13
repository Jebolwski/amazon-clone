import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent {
  public id!: string;
  notyf: Notyf = new Notyf();
  constructor(public cartService: CartService, private route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    cartService.getCartsProducts(this.id);
  }

  buyCart() {
    this.cartService.buyTheCart(this.cartService.cart.id);
  }

  notyfCalis() {
    this.notyf.error('Kartta ürün yok. 😒');
  }
}
