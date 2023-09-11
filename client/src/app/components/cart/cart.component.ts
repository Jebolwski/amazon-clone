import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { AuthService } from 'src/app/services/auth.service';
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

  toggleStatus(productId: string, cartId: string) {
    this.cartService
      .toggleStatus(productId, cartId)
      .subscribe((res: boolean) => {
        this.cartService.getCartsProducts(this.id);
      });
  }

  buyCart() {
    this.cartService.buyTheCart(this.cartService.cart.id);
  }

  notyfCalis() {
    this.notyf.error('Kartta ürün yok. 😒');
  }
}
