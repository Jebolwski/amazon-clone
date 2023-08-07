import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Notyf } from 'notyf';
import { CreditCartService } from 'src/app/services/credit-cart.service';

@Component({
  selector: 'app-add-credit-cart',
  templateUrl: './add-credit-cart.component.html',
  styleUrls: ['./add-credit-cart.component.scss'],
})
export class AddCreditCartComponent {
  constructor(private creditCartService: CreditCartService) {}
  addCreditCartForm: FormGroup = new FormGroup({
    nameSurname: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
    cartNumber: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(28),
    ]),
    cvvNumber: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(3),
    ]),
    month: new FormControl('', [
      Validators.required,
      Validators.min(1),
      Validators.max(12),
      Validators.minLength(1),
      Validators.maxLength(2),
    ]),
    year: new FormControl('', [
      Validators.required,
      Validators.min(23),
      Validators.max(35),
      Validators.minLength(2),
      Validators.maxLength(2),
    ]),
  });
  notyf: Notyf = new Notyf();
  submitIt() {
    if (
      !this.nameSurname?.errors &&
      !this.cartNumber?.errors &&
      !this.cvvNumber?.errors &&
      !this.month?.errors &&
      !this.year?.errors
    ) {
      let data: any = {
        nameSurname: this.nameSurname?.value,
        cartNumber: this.cartNumber?.value,
        cvvNumber: this.cvvNumber?.value,
        expDate: this.month?.value + '/' + this.year?.value,
      };
      this.creditCartService.addCreditCart(data).subscribe((res: any) => {});
    } else {
      console.log(
        !this.nameSurname?.errors,
        !this.cartNumber?.errors,
        !this.cvvNumber?.errors,
        !this.month?.errors,
        !this.year?.errors
      );

      if (this.addCreditCartForm.errors) {
        this.notyf.error(this.addCreditCartForm.errors[0]?.message);
      }
    }
  }

  get nameSurname() {
    return this.addCreditCartForm.get('nameSurname');
  }
  get cartNumber() {
    return this.addCreditCartForm.get('cartNumber');
  }
  get cvvNumber() {
    return this.addCreditCartForm.get('cvvNumber');
  }
  get month() {
    return this.addCreditCartForm.get('month');
  }
  get year() {
    return this.addCreditCartForm.get('year');
  }
}
