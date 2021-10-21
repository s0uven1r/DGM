import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseTypeCreateComponent } from './course-type-create.component';

describe('CourseTypeCreateComponent', () => {
  let component: CourseTypeCreateComponent;
  let fixture: ComponentFixture<CourseTypeCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CourseTypeCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseTypeCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
