import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CreditCartService } from 'src/app/services/credit-cart.service';

@Component({
  selector: 'app-delete-credit-cart',
  templateUrl: './delete-credit-cart.component.html',
  styleUrls: ['./delete-credit-cart.component.scss'],
})
export class DeleteCreditCartComponent {
  id: string | null;
  creditCart!: any;
  constructor(
    public creditCartService: CreditCartService,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id);

    creditCartService.getCreditCartById(this.id!).subscribe((res: any) => {
      this.creditCart = res;
    });
  }
}
