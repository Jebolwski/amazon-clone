import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-delete-category',
  templateUrl: './delete-category.component.html',
  styleUrls: ['./delete-category.component.scss'],
})
export class DeleteCategoryComponent implements OnInit {
  id: string | null;
  constructor(
    public category: CategoryService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  delete() {
    let result = this.category.deleteCategory(this.id || '');
    this.router.navigate(['/all-categories']);
  }

  back(): void {
    this.location.back();
  }

  ngOnInit(): void {
    this.category.getCategoryById(this.id || '');
  }
}
