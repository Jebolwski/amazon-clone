import { Component } from '@angular/core';
import { Address } from 'src/app/interfaces/address';
import { AddressService } from 'src/app/services/address.service';

@Component({
  selector: 'app-addresses',
  templateUrl: './addresses.component.html',
  styleUrls: ['./addresses.component.scss'],
})
export class AddressesComponent {
  addresses: Address[] = [];

  constructor(private addressService: AddressService) {
    this.addressService.getYourAddresses().subscribe((res: any) => {
      this.addresses = res;
    });
  }
}
