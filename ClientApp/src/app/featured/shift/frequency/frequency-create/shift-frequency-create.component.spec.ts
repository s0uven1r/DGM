import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftFrequencyCreateComponent } from './shift-frequency-create.component';

describe('ShiftFrequencyCreateComponent', () => {
  let component: ShiftFrequencyCreateComponent;
  let fixture: ComponentFixture<ShiftFrequencyCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShiftFrequencyCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShiftFrequencyCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
