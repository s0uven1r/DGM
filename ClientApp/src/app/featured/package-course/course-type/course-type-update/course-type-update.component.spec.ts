import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseTypeUpdateComponent } from './course-type-update.component';

describe('CourseTypeUpdateComponent', () => {
  let component: CourseTypeUpdateComponent;
  let fixture: ComponentFixture<CourseTypeUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CourseTypeUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseTypeUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
