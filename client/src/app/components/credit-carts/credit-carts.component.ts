import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CreditCartService } from 'src/app/services/credit-cart.service';

@Component({
  selector: 'app-credit-carts',
  templateUrl: './credit-carts.component.html',
  styleUrls: ['./credit-carts.component.scss'],
})
export class CreditCartsComponent {
  creditCarts: any[] = [];
  constructor(
    private creditCartService: CreditCartService,
    public authService: AuthService
  ) {
    this.creditCartService.getYourCreditCarts().subscribe((res) => {
      this.creditCarts = res;
    });
  }
}
