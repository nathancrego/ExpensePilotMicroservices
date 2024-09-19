import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpensestatusListComponent } from './expensestatus-list.component';

describe('ExpensestatusListComponent', () => {
  let component: ExpensestatusListComponent;
  let fixture: ComponentFixture<ExpensestatusListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ExpensestatusListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpensestatusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
