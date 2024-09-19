import { Component, OnDestroy, OnInit } from '@angular/core';
import { Department } from '../models/department.model';
import { Subscription } from 'rxjs';
import { DepartmentService } from '../services/department.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EditDepartment } from '../models/edit-department.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-department',
  templateUrl: './edit-department.component.html',
  styleUrl: './edit-department.component.css'
})
export class EditDepartmentComponent implements OnInit, OnDestroy {

  dptID: number | null = null;
  department?: Department;

  paramSubscription?: Subscription;
  routeSubscription?: Subscription;
  updateDepartmentSubscription?: Subscription;
  deleteDepartmentSubscription?: Subscription;

  constructor(private departmentService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }


  ngOnInit(): void {
    //Sunscription to map the id and also convert id to string and then back to number
    this.routeSubscription = this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        const idParam = params.get('id');
        this.dptID = idParam !== null ? Number(idParam) : null;

        //Fetch Department from API
        if (this.dptID !== null) {
          this.departmentService.getDepartmentById(this.dptID)
            .subscribe({
              next: (response) => {
                this.department = response;
              },
              error: (err) => {
                console.error('Error fetching department:', err);
              }
            });
        } else {
          console.warn('No valid department ID found in route parameters.');
        }
      },
      error: (err) => {
        console.error('Error retrieving route parameters:', err);
      }
    });
  }

  onFormSubmit(editdeptForm: NgForm): void {
    if (editdeptForm.valid) {
      //Convert Model to request object
      if (this.department && this.dptID) {
        var editDepartment: EditDepartment = {
          departmentName: this.department?.departmentName,
          lastUpdated: new Date()
        };
        this.updateDepartmentSubscription = this.departmentService.updateDepartment(this.dptID, editDepartment)
          .subscribe({
            next: (response) => {
              this.router.navigateByUrl('/admin/department')
            }
          });
      }
    }
    else {
      Object.keys(editdeptForm.controls).forEach(field => {
        const control = editdeptForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }
  onDelete(): void {
    if (this.dptID) {
      //Call service to delete department
      this.deleteDepartmentSubscription = this.departmentService.deleteDepartment(this.dptID)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/department')
          }
        })
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/department');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateDepartmentSubscription?.unsubscribe();
    this.deleteDepartmentSubscription?.unsubscribe();
  }
}
