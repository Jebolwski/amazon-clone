import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditCartsComponent } from './credit-carts.component';

describe('CreditCartsComponent', () => {
  let component: CreditCartsComponent;
  let fixture: ComponentFixture<CreditCartsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreditCartsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreditCartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
