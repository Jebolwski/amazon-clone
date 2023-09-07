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
  public ArchivedBoughts: Bought[] = [];
  public archived: boolean = false;
  constructor(private boughtService: BoughtService) {
    this.getBoughts();
    this.getArchivedBoughts();
  }

  toggleArchived() {
    this.archived = !this.archived;
  }

  getBoughts() {
    this.boughtService.getAllBoughts().subscribe((res: any) => {
      this.boughts = res;
      this.boughts.forEach((element: Bought) => {
        let productsArray: Product[] = [];
        let date1 =
          new Date(element.timeBought).getDate() - new Date().getDate();
        console.log(date1);
        if (date1 < 15) {
          element.refundable = true;
        } else {
          element.refundable = false;
        }

        let tarih =
          new Date(element.timeBought).getUTCDate() +
          ' ' +
          new Date(element.timeBought).toLocaleDateString('tr-TR', {
            month: 'long',
          }) +
          ' ' +
          new Date(element.timeBought).getFullYear() +
          ' ' +
          new Date(element.timeBought).getHours() +
          ' : ' +
          new Date(element.timeBought).getUTCMinutes();

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

  getArchivedBoughts() {
    this.boughtService.getAllArchivedBoughts().subscribe((res: any) => {
      this.ArchivedBoughts = res;
      this.ArchivedBoughts.forEach((element: Bought) => {
        let productsArray: Product[] = [];
        let date1 =
          new Date(element.timeBought).getDate() - new Date().getDate();
        console.log(date1);
        if (date1 < 15) {
          element.refundable = true;
        } else {
          element.refundable = false;
        }

        let tarih =
          new Date(element.timeBought).getUTCDate() +
          ' ' +
          new Date(element.timeBought).toLocaleDateString('tr-TR', {
            month: 'long',
          }) +
          ' ' +
          new Date(element.timeBought).getFullYear() +
          ' ' +
          new Date(element.timeBought).getHours() +
          ' : ' +
          new Date(element.timeBought).getUTCMinutes();

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
  }

  deleteBoughts(id: string) {
    this.boughtService.deleteBoughts(id).subscribe(() => {
      this.boughts = [];
      this.getBoughts();
    });
  }
}
