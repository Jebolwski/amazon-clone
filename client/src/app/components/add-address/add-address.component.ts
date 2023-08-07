import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Notyf } from 'notyf';
import { AddressService } from 'src/app/services/address.service';

@Component({
  selector: 'app-add-address',
  templateUrl: './add-address.component.html',
  styleUrls: ['./add-address.component.scss'],
})
export class AddAddressComponent {
  notyf: Notyf = new Notyf();
  constructor(private addressService: AddressService) {}
  addAddressForm: FormGroup = new FormGroup({
    city: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
    hood: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(28),
    ]),
    apartmentName: new FormControl('', [
      Validators.required,
      Validators.minLength(4),
      Validators.maxLength(45),
    ]),
    apartmentNo: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(3),
    ]),
    floor: new FormControl('', [Validators.required, Validators.maxLength(2)]),
  });

  submitForm() {
    console.log('mesi');
    if (
      !this.city?.errors &&
      !this.hood?.errors &&
      !this.apartmentNo?.errors &&
      !this.apartmentName?.errors &&
      !this.floor?.errors
    ) {
      this.addressService
        .addAddress(this.addAddressForm.value)
        .subscribe((res: any) => {});
    } else {
      console.log(
        !this.city?.errors,
        !this.hood?.errors,
        !this.apartmentNo?.errors,
        !this.apartmentName?.errors,
        !this.floor?.errors
      );

      if (this.addAddressForm.errors) {
        this.notyf.error(this.addAddressForm.errors[0]?.message);
      }
    }
  }

  get city() {
    return this.addAddressForm.get('city');
  }
  get hood() {
    return this.addAddressForm.get('hood');
  }
  get apartmentName() {
    return this.addAddressForm.get('apartmentName');
  }
  get apartmentNo() {
    return this.addAddressForm.get('apartmentNo');
  }
  get floor() {
    return this.addAddressForm.get('floor');
  }
}
