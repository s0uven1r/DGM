import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualShiftComponent } from './individual-shift.component';

describe('IndividualShiftComponent', () => {
  let component: IndividualShiftComponent;
  let fixture: ComponentFixture<IndividualShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualShiftComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndividualShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
