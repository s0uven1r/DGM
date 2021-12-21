import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftFrequencyComponent } from './shift-frequency.component';

describe('ShiftFrequencyComponent', () => {
  let component: ShiftFrequencyComponent;
  let fixture: ComponentFixture<ShiftFrequencyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShiftFrequencyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShiftFrequencyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
