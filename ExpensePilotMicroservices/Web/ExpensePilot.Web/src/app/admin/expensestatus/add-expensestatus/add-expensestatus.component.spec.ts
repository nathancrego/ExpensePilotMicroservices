import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExpensestatusComponent } from './add-expensestatus.component';

describe('AddExpensestatusComponent', () => {
  let component: AddExpensestatusComponent;
  let fixture: ComponentFixture<AddExpensestatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddExpensestatusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddExpensestatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
