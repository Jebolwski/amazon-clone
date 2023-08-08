import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CreditCartService } from 'src/app/services/credit-cart.service';

@Component({
  selector: 'app-credit-cart-detail',
  templateUrl: './credit-cart-detail.component.html',
  styleUrls: ['./credit-cart-detail.component.scss'],
})
export class CreditCartDetailComponent {
  id: string;
  creditCart: any;
  constructor(
    private route: ActivatedRoute,
    public creditCartService: CreditCartService
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    console.log(this.id);

    this.creditCartService
      .getCreditCartById(this.id)
      .subscribe((creditcart: any) => {
        this.creditCart = creditcart;
      });
  }
}
