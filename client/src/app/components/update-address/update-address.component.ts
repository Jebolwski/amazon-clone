import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { Address } from 'src/app/interfaces/address';
import { AddressService } from 'src/app/services/address.service';

@Component({
  selector: 'app-update-address',
  templateUrl: './update-address.component.html',
  styleUrls: ['./update-address.component.scss'],
})
export class UpdateAddressComponent {
  id: string | null;
  address!: Address;
  notyf: Notyf = new Notyf();
  constructor(
    private addressService: AddressService,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    this.addressService.getById(this.id!).subscribe((res: any) => {
      this.address = res;
      this.city?.setValue(this.address.city);
      this.hood?.setValue(this.address.hood);
      this.floor?.setValue(this.address.floor);
      this.apartmentName?.setValue(this.address.apartmentName);
      this.apartmentNo?.setValue(this.address.apartmentNo);
    });
  }
  updateAddressForm: FormGroup = new FormGroup({
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
    if (
      !this.city?.errors &&
      !this.hood?.errors &&
      !this.apartmentNo?.errors &&
      !this.apartmentName?.errors &&
      !this.floor?.errors
    ) {
      let data: any = {
        city: this.city?.value,
        hood: this.hood?.value,
        apartmentName: this.apartmentName?.value,
        apartmentNo: this.apartmentNo?.value,
        floor: this.floor?.value,
        id: this.id,
      };
      this.addressService.updateAddress(data).subscribe((res: any) => {});
    } else {
      if (this.updateAddressForm.errors) {
        this.notyf.error(this.updateAddressForm.errors[0]?.message);
      }
    }
  }

  get city() {
    return this.updateAddressForm.get('city');
  }
  get hood() {
    return this.updateAddressForm.get('hood');
  }
  get apartmentName() {
    return this.updateAddressForm.get('apartmentName');
  }
  get apartmentNo() {
    return this.updateAddressForm.get('apartmentNo');
  }
  get floor() {
    return this.updateAddressForm.get('floor');
  }
}
