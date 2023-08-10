import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateCreditCartComponent } from './update-credit-cart.component';

describe('UpdateCreditCartComponent', () => {
  let component: UpdateCreditCartComponent;
  let fixture: ComponentFixture<UpdateCreditCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateCreditCartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateCreditCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
