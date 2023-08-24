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
    this.getBoughts();
  }

  getBoughts() {
    this.boughtService.getAllBoughts().subscribe((res: any) => {
      this.boughts = res;
      this.boughts.forEach((element: Bought) => {
        let productsArray: Product[] = [];
        let tarih =
          new Date(element.timeBought).getDay() +
          ' ' +
          new Date(element.timeBought).toLocaleDateString('tr-TR', {
            month: 'long',
          }) +
          ' ' +
          new Date(element.timeBought).getFullYear();

        element.timeBought = tarih;
        element.products.forEach((product: Product) => {
          if (
            productsArray.filter(
              (x: Product) => x.productId == product.productId
            ).length == 0
          ) {
            product.count = 1;
            productsArray.push(product);
          } else {
            productsArray.filter(
              (x: Product) => x.boughtId == product.boughtId
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

        productsArray?.forEach((product) => {
          product.price = f.format(parseFloat(product.price));
        });
        element.products = productsArray;
      });
    });
    console.log(this.boughts);
  }

  deleteBoughts(id: string) {
    this.boughtService.deleteBoughts(id).subscribe(() => {
      this.boughts = [];
      this.getBoughts();
    });
  }
}
