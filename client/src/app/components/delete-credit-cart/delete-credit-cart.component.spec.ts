import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteCreditCartComponent } from './delete-credit-cart.component';

describe('DeleteCreditCartComponent', () => {
  let component: DeleteCreditCartComponent;
  let fixture: ComponentFixture<DeleteCreditCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteCreditCartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteCreditCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
