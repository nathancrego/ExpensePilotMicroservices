import { Component, OnDestroy } from '@angular/core';
import { AddExpenseStatus } from '../models/add-expensestatus.model';
import { Subscription } from 'rxjs';
import { ExpensestatusService } from '../services/expensestatus.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-expensestatus',
  templateUrl: './add-expensestatus.component.html',
  styleUrl: './add-expensestatus.component.css'
})
export class AddExpensestatusComponent implements OnDestroy {

  status: AddExpenseStatus;
  private addExpenseStatusSubscription?: Subscription;

  constructor(private expensestatusService: ExpensestatusService, private router: Router) {
    this.status = {
      statusName: '',
      lastUpdated: new Date()
    };
  }

  onFormSubmit(statusForm: NgForm): void {
    if (statusForm.valid) {
      this.addExpenseStatusSubscription = this.expensestatusService.createExpenseStatus(this.status)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/expensestatus')
          }
        });
    }
    else {
      Object.keys(statusForm.controls).forEach(field => {
        const control = statusForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/expensestatus');
  }

  ngOnDestroy(): void {
    this.addExpenseStatusSubscription?.unsubscribe();
  }



}
