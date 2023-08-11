import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';
import { CreditCartService } from 'src/app/services/credit-cart.service';

@Component({
  selector: 'app-finish-buying-cart',
  templateUrl: './finish-buying-cart.component.html',
  styleUrls: ['./finish-buying-cart.component.scss'],
})
export class FinishBuyingCartComponent {
  public id!: string;
  public creditCarts: any[] = [];
  constructor(
    public cartService: CartService,
    private route: ActivatedRoute,
    public creditCartService: CreditCartService
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    cartService.getCartsProducts(this.id);
    creditCartService.getYourCreditCarts().subscribe((res: any) => {
      this.creditCarts = res;
    });
  }

  buyCart() {
    this.cartService.buyTheCart(this.cartService.cart.id);
  }
}
