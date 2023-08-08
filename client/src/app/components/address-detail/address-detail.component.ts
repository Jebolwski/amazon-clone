import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Address } from 'src/app/interfaces/address';
import { AddressService } from 'src/app/services/address.service';
import { AuthService } from 'src/app/services/auth.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-address-detail',
  templateUrl: './address-detail.component.html',
  styleUrls: ['./address-detail.component.scss'],
})
export class AddressDetailComponent {
  id: string;
  address!: Address;
  constructor(
    private route: ActivatedRoute,
    public addressService: AddressService
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    console.log(this.id);

    this.addressService.getById(this.id).subscribe((address: any) => {
      this.address = address;
    });
  }
}
