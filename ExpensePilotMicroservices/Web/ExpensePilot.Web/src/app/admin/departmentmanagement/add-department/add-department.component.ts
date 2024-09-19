import { Component, OnDestroy } from '@angular/core';
import { AddDepartment } from '../models/add-department.model';
import { Subscription } from 'rxjs';
import { DepartmentService } from '../services/department.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-department',
  templateUrl: './add-department.component.html',
  styleUrl: './add-department.component.css'
})
export class AddDepartmentComponent implements OnDestroy {

  department: AddDepartment;
  private addDepartmentSubscription?: Subscription;

  constructor(private departmentService: DepartmentService, private router: Router) {
    this.department = {
      departmentName: '',
      lastUpdated: new Date()
    };
  }

  onFormSubmit(deptForm: NgForm): void {
    if (deptForm.valid) {
      this.addDepartmentSubscription = this.departmentService.createDepartment(this.department)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/department');
          }
        });
    }
    else {
      Object.keys(deptForm.controls).forEach(field => {
        const control = deptForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/department');
  }

  ngOnDestroy(): void {
    this.addDepartmentSubscription?.unsubscribe();
  }
}
