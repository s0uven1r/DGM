import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionEntryCreateComponent } from './transaction-entry-create.component';

describe('TransactionEntryCreateComponent', () => {
  let component: TransactionEntryCreateComponent;
  let fixture: ComponentFixture<TransactionEntryCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransactionEntryCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionEntryCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
