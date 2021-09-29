import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageUpdateComponent } from './package-update.component';

describe('PackageUpdateComponent', () => {
  let component: PackageUpdateComponent;
  let fixture: ComponentFixture<PackageUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PackageUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PackageUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
