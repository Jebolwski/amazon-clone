import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessfullyBoughtComponent } from './successfully-bought.component';

describe('SuccessfullyBoughtComponent', () => {
  let component: SuccessfullyBoughtComponent;
  let fixture: ComponentFixture<SuccessfullyBoughtComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuccessfullyBoughtComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuccessfullyBoughtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
