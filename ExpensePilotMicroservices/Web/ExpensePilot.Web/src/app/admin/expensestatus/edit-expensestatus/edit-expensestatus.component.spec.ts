import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditExpensestatusComponent } from './edit-expensestatus.component';

describe('EditExpensestatusComponent', () => {
  let component: EditExpensestatusComponent;
  let fixture: ComponentFixture<EditExpensestatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditExpensestatusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditExpensestatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
