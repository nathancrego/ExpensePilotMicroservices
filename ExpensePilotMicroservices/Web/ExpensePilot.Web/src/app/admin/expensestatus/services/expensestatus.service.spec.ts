import { TestBed } from '@angular/core/testing';

import { ExpensestatusService } from './expensestatus.service';

describe('ExpensestatusService', () => {
  let service: ExpensestatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExpensestatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
