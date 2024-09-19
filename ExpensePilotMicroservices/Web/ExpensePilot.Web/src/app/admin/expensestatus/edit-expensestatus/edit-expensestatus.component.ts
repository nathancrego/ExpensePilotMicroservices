import { Component, OnDestroy, OnInit } from '@angular/core';
import { ExpenseStatus } from '../models/expensestatus.model';
import { Subscription } from 'rxjs';
import { ExpensestatusService } from '../services/expensestatus.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { EditExpenseStatus } from '../models/edit-expensestatus.model';

@Component({
  selector: 'app-edit-expensestatus',
  templateUrl: './edit-expensestatus.component.html',
  styleUrl: './edit-expensestatus.component.css'
})
export class EditExpensestatusComponent implements OnInit, OnDestroy {

  statusID: number | null = null;
  status?: ExpenseStatus;

  paramSubscription?: Subscription;
  routeSubscription?: Subscription;
  updateExpenseStatusSubscription?: Subscription;
  deleteExpenseStatusSubscription?: Subscription;

  constructor(private expensestatusService: ExpensestatusService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    //Sunscription to map the id and also convert id to string and then back to number
    this.routeSubscription = this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        const idParam = params.get('id');
        this.statusID = idParam !== null ? Number(idParam) : null;

        //Fetch Expense status from API
        if (this.statusID !== null) {
          this.expensestatusService.getStatusById(this.statusID)
            .subscribe({
              next: (response) => {
                this.status = response;
              },
              error: (err) => {
                console.error('Error fetching expense category:', err);
              }
            });
        } else {
          console.warn('No valid expense status ID found in route parameters.');
        }
      },
      error: (err) => {
        console.error('Error retrieving route parameters:', err);
      }
    });
  }

  onFormSubmit(editstatusForm: NgForm): void {
    if (editstatusForm.valid) {
      //Convert Model to request object
      if (this.status && this.statusID) {
        var editStatus: EditExpenseStatus = {
          statusName: this.status?.statusName,
          lastUpdated: new Date()
        };
        this.updateExpenseStatusSubscription = this.expensestatusService.updateStatus(this.statusID, editStatus)
          .subscribe({
            next: (response) => {
              this.router.navigateByUrl('/admin/expensestatus')
            }
          });
      }
    }
    else {
      Object.keys(editstatusForm.controls).forEach(field => {
        const control = editstatusForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onDelete(): void {
    if (this.statusID) {
      //Call service to delete department
      this.deleteExpenseStatusSubscription = this.expensestatusService.deleteStatus(this.statusID)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/expensestatus')
          }
        })
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/expensestatus');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateExpenseStatusSubscription?.unsubscribe();
    this.deleteExpenseStatusSubscription?.unsubscribe();
  }
}
