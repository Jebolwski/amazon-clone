import { Component } from '@angular/core';
import { Bought, Product } from 'src/app/interfaces/bought';
import { BoughtService } from 'src/app/services/bought.service';

@Component({
  selector: 'app-boughts',
  templateUrl: './boughts.component.html',
  styleUrls: ['./boughts.component.scss'],
})
export class BoughtsComponent {
  public boughts: Bought[] = [];
  constructor(private boughtService: BoughtService) {
    this.boughtService.getAllBoughts().subscribe((res: any) => {
      this.boughts = res;
      this.boughts.forEach((element) => {
        let productsArray: Product[] = [];
        element.products.forEach((product: Product) => {
          if (
            productsArray.filter((x: Product) => x.id == product.id).length == 0
          ) {
            product.count = 1;
            productsArray.push(product);
          } else {
            productsArray.filter(
              (x: Product) => x.id == product.id
            )[0].count! += 1;
          }
        });
        const f = new Intl.NumberFormat('tr-TR', {
          style: 'currency',
          currency: 'TRY',
          minimumFractionDigits: 2,
        });
        productsArray = productsArray.sort((a, b) => {
          return a.name.length - b.name.length;
        });

        element.products?.forEach((product) => {
          product.price = f.format(parseFloat(product.price));
        });
        console.log(productsArray);

        element.products = productsArray;
      });
    });
  }
}
