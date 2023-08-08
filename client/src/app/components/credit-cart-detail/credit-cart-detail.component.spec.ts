import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditCartDetailComponent } from './credit-cart-detail.component';

describe('CreditCartDetailComponent', () => {
  let component: CreditCartDetailComponent;
  let fixture: ComponentFixture<CreditCartDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreditCartDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreditCartDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
