import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Notyf } from 'notyf';
import { Address } from 'src/app/interfaces/address';
import { AddressService } from 'src/app/services/address.service';
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
  public addresses: Address[] = [];
  selectedAddress: string | null = null;
  private notyf: Notyf = new Notyf();
  selectedCreditCart: string | null = null;
  constructor(
    public cartService: CartService,
    private route: ActivatedRoute,
    public creditCartService: CreditCartService,
    public addressService: AddressService,
    public router: Router
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    cartService.getCartsProducts(this.id);
    creditCartService.getYourCreditCarts().subscribe((res: any) => {
      this.creditCarts = res;
    });
    addressService.getYourAddresses().subscribe((res: any) => {
      this.addresses = res;
    });
  }

  pickCCart(id: string, event: Event) {
    let childs = (event.target as HTMLElement).parentNode?.children;
    for (let i = 0; i < childs!.length; i++) {
      childs![i].classList.remove('border-stone-500');
    }
    (event.target as HTMLElement).classList.add('border-stone-500');
    this.selectedCreditCart = id;
  }

  pickAddress(id: string, event: Event) {
    let childs = (event.target as HTMLElement).parentNode?.children;
    for (let i = 0; i < childs!.length; i++) {
      childs![i].classList.remove('border-stone-500');
    }
    (event.target as HTMLElement).classList.add('border-stone-500');
    this.selectedAddress = id;
  }

  buyCart() {
    if (this.selectedAddress != null && this.selectedCreditCart != null) {
      this.cartService.buyTheCart(this.cartService.cart.id);
      this.router.navigate(['/succesfully-bougth']);
    } else {
      this.notyf.error('Adres ve kredi kartını seçiniz. 😥');
    }
  }
}
