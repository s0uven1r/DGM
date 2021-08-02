import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountHeadCreateComponent } from './account-head-create.component';

describe('AccountHeadCreateComponent', () => {
  let component: AccountHeadCreateComponent;
  let fixture: ComponentFixture<AccountHeadCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountHeadCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountHeadCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
