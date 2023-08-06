import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AddressService } from 'src/app/services/address.service';

@Component({
  selector: 'app-add-address',
  templateUrl: './add-address.component.html',
  styleUrls: ['./add-address.component.scss'],
})
export class AddAddressComponent {
  constructor(private addressService: AddressService) {}
  public addAddressForm: FormGroup = new FormGroup({
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

  submitForm(event: any) {
    event.preventDefault();
    if (
      !this.city?.errors &&
      !this.hood?.errors &&
      !this.apartmentNo?.errors &&
      !this.apartmentName?.errors &&
      !this.floor?.errors
    ) {
      this.addressService.addAddress(this.addAddressForm.value);
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
