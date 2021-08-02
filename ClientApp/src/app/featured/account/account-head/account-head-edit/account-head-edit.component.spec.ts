import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountHeadEditComponent } from './account-head-edit.component';

describe('AccountHeadEditComponent', () => {
  let component: AccountHeadEditComponent;
  let fixture: ComponentFixture<AccountHeadEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountHeadEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountHeadEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
