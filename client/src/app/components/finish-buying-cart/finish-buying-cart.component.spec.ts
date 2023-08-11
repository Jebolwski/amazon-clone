import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinishBuyingCartComponent } from './finish-buying-cart.component';

describe('FinishBuyingCartComponent', () => {
  let component: FinishBuyingCartComponent;
  let fixture: ComponentFixture<FinishBuyingCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinishBuyingCartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FinishBuyingCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
