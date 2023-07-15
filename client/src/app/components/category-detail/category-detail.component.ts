import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductCategory } from 'src/app/interfaces/product';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss'],
})
export class CategoryDetailComponent {
  id: string;
  constructor(
    private route: ActivatedRoute,
    public categoryService: CategoryService
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    this.categoryService.getCategoryById(this.id);
    console.log(this.categoryService.category);
  }
}
