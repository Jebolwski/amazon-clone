import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Address } from 'src/app/interfaces/address';
import { AddressService } from 'src/app/services/address.service';

@Component({
  selector: 'app-delete-address',
  templateUrl: './delete-address.component.html',
  styleUrls: ['./delete-address.component.scss'],
})
export class DeleteAddressComponent {
  id: string | null;
  address!: Address;
  constructor(
    public addressService: AddressService,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id);

    addressService.getById(this.id!).subscribe((res: any) => {
      this.address = res;
    });
  }
}
