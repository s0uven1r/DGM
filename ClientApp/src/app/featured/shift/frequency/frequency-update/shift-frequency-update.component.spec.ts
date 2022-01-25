import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftFrequencyUpdateComponent } from './shift-frequency-update.component';

describe('ShiftFrequencyUpdateComponent', () => {
  let component: ShiftFrequencyUpdateComponent;
  let fixture: ComponentFixture<ShiftFrequencyUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShiftFrequencyUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShiftFrequencyUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
