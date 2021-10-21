import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DescriptiveImageComponent } from './descriptive-image.component';

describe('DescriptiveImageComponent', () => {
  let component: DescriptiveImageComponent;
  let fixture: ComponentFixture<DescriptiveImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DescriptiveImageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DescriptiveImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
