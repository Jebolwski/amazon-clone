import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoughtsComponent } from './boughts.component';

describe('BoughtsComponent', () => {
  let component: BoughtsComponent;
  let fixture: ComponentFixture<BoughtsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BoughtsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoughtsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
