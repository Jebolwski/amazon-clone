import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { addProductCategory } from 'src/app/interfaces/addProductCategory';
import { addProductPhoto } from 'src/app/interfaces/addProductPhoto';
import { Photo, Product, ProductCategory } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss'],
})
export class AddProductComponent implements OnInit {
  public addProductForm: FormGroup = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
    price: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(20),
    ]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(10),
      Validators.maxLength(200),
    ]),
  });

  jsonData: {
    name: string;
    price: number;
    description: string;
    photos: addProductPhoto[];
    productCategories: addProductCategory[];
  } = {
    name: '',
    price: 0,
    description: '',
    photos: [],
    productCategories: [],
  };
  numberOfPhoto: number = 0;

  constructor(public product: ProductService) {}

  ngOnInit(): void {
    this.product.getAllProductCategories();
  }

  toggleCategoryToData(category: ProductCategory, e: Event) {
    if ((e.target as HTMLElement).classList.contains('bg-stone-600')) {
      (e.target as HTMLElement).classList.remove('bg-stone-600');
      (e.target as HTMLElement).classList.remove('text-white');
      this.removeFromData(category);
    } else {
      (e.target as HTMLElement).classList.add('bg-stone-600');
      (e.target as HTMLElement).classList.add('text-white');
      this.addToData(category);
    }
  }

  addToData(category: ProductCategory) {
    this.jsonData['productCategories'] = [
      ...this.jsonData['productCategories'],
      { id: category.id },
    ];
    console.log(this.jsonData);
  }

  removeFromData(category: ProductCategory) {
    let index: number = this.jsonData['productCategories'].indexOf({
      id: category.id,
    });
    this.jsonData['productCategories'].splice(index, 1);
    console.log(this.jsonData);
  }

  constructData(): {
    name: string;
    price: number;
    description: string;
    photos: addProductPhoto[];
    productCategories: addProductCategory[];
  } {
    let photosArr: { photoUrl: string }[] = [];
    let photoItems = document.querySelectorAll('.link');
    photoItems.forEach((element) => {
      photosArr.push({ photoUrl: (element as HTMLInputElement).value });
    });
    this.jsonData = {
      name: this.addProductForm.get('name')?.value,
      price: this.addProductForm.get('price')?.value,
      description: this.addProductForm.get('description')?.value,
      photos: photosArr,
      productCategories: this.jsonData['productCategories'],
    };
    return this.jsonData;
  }

  addIt() {
    let json = this.constructData();
    this.product.addProduct(json);
  }

  addNewLink() {
    let links_div = document.querySelector('.photo-links');
    let num = links_div?.children.length;
    this.numberOfPhoto = num || 0;
    let newInput = document.createElement('div');
    if (
      num != undefined &&
      links_div != null &&
      links_div?.children.length < 5
    ) {
      newInput.innerHTML =
        '<div class="flex items-center gap-2"><input type="text" formControlName="price" placeholder="Fotoğraf linki" class="p-2 link link' +
        String(num + 1) +
        ' outline-none border shadow-md border-stone-200 rounded-md mt-1 flex-1 w-full" /> <i class="fa-solid fa-trash fa-lg text-red-500 cursor-pointer"></i> </div>';
      newInput.querySelector('.fa-trash')?.addEventListener('click', (e) => {
        newInput.remove();
        links_div = document.querySelector('.photo-links');
        num = links_div?.children.length;
        this.numberOfPhoto = num || 0;
      });

      links_div?.appendChild(newInput);
    }
  }
}
