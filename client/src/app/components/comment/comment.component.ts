import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { addProductPhoto } from 'src/app/interfaces/addProductPhoto';
import { Product } from 'src/app/interfaces/product';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent {
  constructor(
    private router: ActivatedRoute,
    public productService: ProductService,
    public authService: AuthService,
    private commentService: CommentService
  ) {}

  public updateProductForm: FormGroup = new FormGroup({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
    comment: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(20),
    ]),
  });

  starCount: number = 0;
  product!: Product;
  id!: string;
  numberOfPhoto: number = 0;

  starEdit(x: number) {
    let stars = document.querySelectorAll('.stars');
    for (let i = 0; i < 5; i++) {
      stars[i].classList.remove('fa-solid');
    }
    for (let i = 0; i < x; i++) {
      stars[i].classList.add('fa-solid');
    }
    this.starCount = x;
  }

  constructData() {
    let photoItems = document.querySelectorAll('.link');
    let photosArr: { photoUrl: string }[] = [];
    photoItems.forEach((element) => {
      photosArr.push({ photoUrl: (element as HTMLInputElement).value });
    });
    let data: {
      title: string;
      comment: string;
      stars: number;
      commentPhotos: addProductPhoto[];
      productId: string;
    } = this.updateProductForm.value;
    data.stars = this.starCount;
    data.productId = this.id;
    data.commentPhotos = photosArr;
    return data;
  }

  addNewLink() {
    let links_div = document.querySelector('.photo-links');
    let num = links_div?.children.length;
    this.numberOfPhoto = num || 0;
    let newInput = document.createElement('div');
    if (
      num != undefined &&
      links_div != null &&
      links_div?.children.length < 4
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

  comment() {
    this.commentService.postComment(this.constructData());
  }

  ngOnInit(): void {
    this.id = this.router.snapshot.paramMap.get('id') || '0';
    console.log(this.id);
    this.productService.getProduct(this.id).subscribe((product: Product) => {
      this.product = product;
    });
  }
}
