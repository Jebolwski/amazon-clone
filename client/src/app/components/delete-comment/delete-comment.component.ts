import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentService } from 'src/app/services/comment.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-delete-comment',
  templateUrl: './delete-comment.component.html',
  styleUrls: ['./delete-comment.component.scss'],
})
export class DeleteCommentComponent {
  id: string;
  constructor(
    public commentService: CommentService,
    private route: ActivatedRoute,
    private location: Location
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    commentService.getComment(this.id);
  }

  back(): void {
    this.location.back();
  }

  delete() {
    let result = this.commentService.deleteComment(this.id || '');
  }
}
