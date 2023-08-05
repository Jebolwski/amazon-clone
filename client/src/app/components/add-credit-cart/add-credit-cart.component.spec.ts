import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCreditCartComponent } from './add-credit-cart.component';

describe('AddCreditCartComponent', () => {
  let component: AddCreditCartComponent;
  let fixture: ComponentFixture<AddCreditCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCreditCartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCreditCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
