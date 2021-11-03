import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditIndividualShiftComponent } from './edit-individual-shift.component';

describe('EditIndividualShiftComponent', () => {
  let component: EditIndividualShiftComponent;
  let fixture: ComponentFixture<EditIndividualShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditIndividualShiftComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditIndividualShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
