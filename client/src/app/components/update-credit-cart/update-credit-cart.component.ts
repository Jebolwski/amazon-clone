import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { CreditCartService } from 'src/app/services/credit-cart.service';

@Component({
  selector: 'app-update-credit-cart',
  templateUrl: './update-credit-cart.component.html',
  styleUrls: ['./update-credit-cart.component.scss'],
})
export class UpdateCreditCartComponent {
  id: string | null;
  creditCart!: any;
  constructor(
    private creditCartService: CreditCartService,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    creditCartService.getCreditCartById(this.id!).subscribe((res: any) => {
      this.creditCart = res;
      this.nameSurname?.setValue(this.creditCart?.nameSurname);
      this.cartNumber?.setValue(this.creditCart?.cartNumber);
      this.cvvNumber?.setValue(this.creditCart?.cvvNumber);
      this.month?.setValue(res.expDate.split('/')[0]);
      this.year?.setValue(res.expDate.split('/')[1]);
    });
  }
  updateCreditCartForm: FormGroup = new FormGroup({
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
        id: this.id,
      };
      this.creditCartService.updateCreditCart(data).subscribe((res: any) => {});
    } else {
      console.log(
        !this.nameSurname?.errors,
        !this.cartNumber?.errors,
        !this.cvvNumber?.errors,
        !this.month?.errors,
        !this.year?.errors
      );

      if (this.updateCreditCartForm.errors) {
        this.notyf.error(this.updateCreditCartForm.errors[0]?.message);
      }
    }
  }

  get nameSurname() {
    return this.updateCreditCartForm.get('nameSurname');
  }
  get cartNumber() {
    return this.updateCreditCartForm.get('cartNumber');
  }
  get cvvNumber() {
    return this.updateCreditCartForm.get('cvvNumber');
  }
  get month() {
    return this.updateCreditCartForm.get('month');
  }
  get year() {
    return this.updateCreditCartForm.get('year');
  }
}
