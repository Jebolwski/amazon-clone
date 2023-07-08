import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { firstValueFrom } from 'rxjs';
import {
  addProductCategory,
  addProductCategoryClass,
} from 'src/app/interfaces/addProductCategory';
import { addProductPhoto } from 'src/app/interfaces/addProductPhoto';
import { Product, ProductCategory } from 'src/app/interfaces/product';
import { Response } from 'src/app/interfaces/response';
import { UpdateProduct } from 'src/app/interfaces/updateProduct';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss'],
})
export class UpdateProductComponent implements OnInit {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  id: string | null;
  numberOfPhoto: number = 0;
  notyf = new Notyf();
  public updateProductForm: FormGroup = new FormGroup({
    name: new FormControl(this.productService?.product?.name, [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
    price: new FormControl(this.productService?.product?.price, [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(20),
    ]),
    description: new FormControl(this.productService?.product?.description, [
      Validators.required,
      Validators.minLength(10),
      Validators.maxLength(200),
    ]),
  });
  jsonData: UpdateProduct = {
    id: '',
    name: '',
    price: 0.0,
    description: '',
    photos: [],
    productCategories: [],
  };

  constructor(
    public productService: ProductService,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
  }
  products: Product[] = [];
  product!: Product | undefined;
  productCategories: ProductCategory[] = [];

  async ngOnInit(): Promise<void> {
    firstValueFrom(this.http.get(this.baseApiUrl + 'Product/' + this.id)).then(
      (res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.product = response.responseModel;
        } else {
          this.notyf.error(response.message);
        }
        firstValueFrom(
          this.http.get(
            this.baseApiUrl + 'ProductCategory/get-all-categories',
            {
              headers: new HttpHeaders().append(
                'Authorization',
                `Bearer ${localStorage.getItem('accessToken')}`
              ),
            }
          )
        ).then((res: any) => {
          let response: Response = res;
          if (response.statusCode === 200) {
            this.productCategories = response.responseModel;
          } else {
            this.notyf.error(response.message);
          }
          this.jsonData.productCategories =
            this.product?.productCategories.map((productCategory) => {
              let prod: addProductCategory = { id: productCategory.id };
              return prod;
            }) || [];

          this.updateProductForm.get('name')?.setValue(this.product?.name);
          this.updateProductForm
            .get('description')
            ?.setValue(this.product?.description);
          this.updateProductForm.get('price')?.setValue(this.product?.price);
        });
      }
    );
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
        'outline-none border shadow-md border-stone-200 rounded-md mt-1 flex-1 w-full" />' +
        ' <i class="fa-solid fa-trash fa-lg text-red-500 cursor-pointer"></i> </div>';
      newInput.querySelector('.fa-trash')?.addEventListener('click', (e) => {
        newInput.remove();
        links_div = document.querySelector('.photo-links');
        num = links_div?.children.length;
        this.numberOfPhoto = num || 0;
      });

      links_div?.appendChild(newInput);
    }
  }

  removePhoto(e: Event) {
    console.log(
      (e.target as HTMLElement).parentNode?.parentNode?.parentNode?.parentNode
    );
    console.log(
      (e.target as HTMLElement).parentNode?.parentNode?.parentNode?.parentNode
        ?.children
    );

    if (
      (e.target as HTMLElement).parentNode?.parentNode?.parentNode?.parentNode
        ?.children.length == 1
    ) {
      this.notyf.success('En az bir tane fotoğraf olması gerekir. 😐');
    } else {
      (
        (e.target as HTMLElement).parentNode?.parentNode
          ?.parentNode as HTMLElement
      ).remove();
    }
  }

  updateIt() {
    console.log(this.constructData());
    this.productService.updateProduct(this.constructData());
  }

  addToData(category: ProductCategory) {
    if (!this.includes1(this.jsonData.productCategories, { id: category.id })) {
      this.jsonData.productCategories = [
        ...this.jsonData.productCategories,
        { id: category.id },
      ];
    }
  }

  includes(
    array: ProductCategory[] | undefined,
    productCategory: ProductCategory
  ): boolean {
    let flag: boolean = false;
    array?.forEach((element) => {
      if (
        element.description == productCategory.description &&
        element.id == productCategory.id &&
        element.name == productCategory.name
      ) {
        flag = true;
        return;
      }
    });
    return flag;
  }

  includes1(
    array: addProductCategory[],
    addProductCategory: addProductCategory
  ): boolean {
    let flag: boolean = false;
    array.forEach((element) => {
      if (element.id == addProductCategory.id) {
        flag = true;
        return;
      }
    });
    return flag;
  }

  indexOf(
    category: { id: string },
    productCategories: { id: string }[]
  ): number {
    for (let i = 0; i < productCategories.length; i++) {
      if (productCategories[i].id == category.id) {
        return i;
      }
    }
    return -1;
  }

  constructData(): UpdateProduct {
    let photosArr: { photoUrl: string }[] = [];
    let photoItems = document.querySelectorAll('.link');
    photoItems.forEach((element) => {
      photosArr.push({ photoUrl: (element as HTMLInputElement).value });
    });
    this.jsonData = {
      id: this.id || '',
      name: this.updateProductForm.get('name')?.value,
      price: this.updateProductForm.get('price')?.value,
      description: this.updateProductForm.get('description')?.value,
      photos: photosArr,
      productCategories: this.jsonData.productCategories,
    };
    return this.jsonData;
  }

  removeFromData(category: ProductCategory) {
    let index: number | undefined = this.indexOf(
      category,
      this.jsonData.productCategories
    );
    console.log(index, this.jsonData.productCategories[index]);

    this.jsonData.productCategories?.splice(index, 1);
  }

  toggleCategoryToData(category: ProductCategory, e: Event) {
    console.log('önce == ', ...this.jsonData.productCategories);

    if ((e.target as HTMLElement).classList.contains('bg-stone-600')) {
      (e.target as HTMLElement).classList.remove('bg-stone-600');
      (e.target as HTMLElement).classList.remove('text-white');
      this.removeFromData(category);
    } else {
      (e.target as HTMLElement).classList.add('bg-stone-600');
      (e.target as HTMLElement).classList.add('text-white');
      this.addToData(category);
    }
    console.log('sonra == ', ...this.jsonData.productCategories);
  }
}
