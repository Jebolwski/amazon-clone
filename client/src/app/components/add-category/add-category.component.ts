import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss'],
})
export class AddCategoryComponent {
  constructor(public category: CategoryService) {}

  public addCategoryForm: FormGroup = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(40),
    ]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(10),
      Validators.maxLength(200),
    ]),
  });

  jsonData: {
    name: string;
    description: string;
  } = {
    name: '',
    description: '',
  };

  constructData(): {
    name: string;
    description: string;
  } {
    this.jsonData = {
      name: this.addCategoryForm.get('name')?.value,
      description: this.addCategoryForm.get('description')?.value,
    };
    return this.jsonData;
  }

  addIt() {
    let jsonData = this.constructData();
    this.category.addCategory(jsonData);
    console.log(jsonData);
  }
}
