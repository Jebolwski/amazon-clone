import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Notyf } from 'notyf';
import { firstValueFrom } from 'rxjs';
import { Comment } from 'src/app/interfaces/product';
import { Response } from 'src/app/interfaces/response';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-update-comment',
  templateUrl: './update-comment.component.html',
  styleUrls: ['./update-comment.component.scss'],
})
export class UpdateCommentComponent {
  numberOfPhoto: number = 0;
  comment!: Comment;
  starCount: number = 0;
  id!: string | null;
  notyf = new Notyf();
  jsonData: {
    id: string;
    comment: string;
    title: string;
    stars: number;
    commentPhotos: { photoUrl: string }[];
  } = {
    id: '',
    comment: '',
    title: '',
    stars: 0,
    commentPhotos: [],
  };
  private baseApiUrl: string = 'http://localhost:5044/api/';
  public updateCommentForm: FormGroup = new FormGroup({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(60),
    ]),
    comment: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(150),
    ]),
  });

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private commentService: CommentService
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  async ngOnInit(): Promise<void> {
    firstValueFrom(
      this.http.get(this.baseApiUrl + 'comment/Comment/' + this.id)
    ).then((res: any) => {
      let response: Response = res;
      if (response.statusCode === 200) {
        this.comment = response.responseModel;
      } else {
        this.notyf.error(response.message);
      }
      this.starEdit(this.comment.stars);
      this.updateCommentForm.get('title')?.setValue(this.comment?.title);
      this.updateCommentForm.get('comment')?.setValue(this.comment?.comment);
      this.updateCommentForm.get('stars')?.setValue(this.comment?.stars);
    });
  }

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

  constructData(): any {
    let photosArr: { photoUrl: string }[] = [];
    let photoItems = document.querySelectorAll('.link');
    photoItems.forEach((element) => {
      photosArr.push({ photoUrl: (element as HTMLInputElement).value });
    });
    this.jsonData = {
      id: this.id || '',
      comment: this.updateCommentForm.get('comment')?.value,
      title: this.updateCommentForm.get('title')?.value,
      stars: this.starCount,
      commentPhotos: photosArr,
    };
    console.log(this.jsonData);

    return this.jsonData;
  }

  updateIt() {
    this.commentService.updateComment(this.constructData());
  }
}
