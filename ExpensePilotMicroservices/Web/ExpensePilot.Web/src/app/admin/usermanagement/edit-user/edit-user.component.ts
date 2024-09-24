import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { catchError, Observable, Subscription } from 'rxjs';
import { Department } from '../../departmentmanagement/models/department.model';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from '../../departmentmanagement/services/department.service';
import { Roles } from '../../rolemanagement/models/roles.model';
import { RolesService } from '../../rolemanagement/services/roles.service';
import { EditUser } from '../models/edit-user.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css'
})
export class EditUserComponent implements OnInit, OnDestroy {

  id: string | null = null;
  user?: User;

  managers$?: Observable<User[]>;
  departments$?: Observable<Department[]>;
  roles$?: Observable<Roles[]>;
  selectedManagerId?: string;
  selectedDepartmentId?: number | null = null;
  selectedRoleId?: string;

  paramSubscription?: Subscription;
  routeSubscription?: Subscription;
  updateUserSubscription?: Subscription;

  constructor(private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private departmentService: DepartmentService, private rolesService: RolesService
  ) {}

  ngOnInit(): void {
    //Subscription to map the id and also convert id to string and then back to number
    this.routeSubscription = this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        //Fetch user from API
        if (this.id !== null) {
          this.userService.getUserById(this.id)
            .subscribe({
              next: (response) => {
                this.user = response;
                this.selectedRoleId = response.roleId;
                this.selectedManagerId = response.managerId;
                this.selectedDepartmentId = response.departmentId || null;

              },
              error: (err) => {
                console.error('Error fetching role:', err);
              }
            });
        } else {
          console.warn('No valid role ID found in route parameters.');
        }
      },
      error: (err) => {
        console.error('Error retrieving route parameters:', err);
      }
    });

    this.managers$ = this.userService.getAllUsers();
    this.departments$ = this.departmentService.getAllDepartments();
    this.roles$ = this.rolesService.getAllRoles();

  }

  onSubmit(editUserForm: NgForm): void {
    if (editUserForm.valid) {
      //Convert Model to request object
      if (this.user && this.id) {
        var editUser: EditUser = {
          userName: this.user?.userName,
          fname: this.user?.fname,
          lname: this.user?.lname,
          email: this.user?.email,
          phoneNumber: this.user?.phoneNumber,
          managerId:this.selectedManagerId,
          managerName: this.user?.managerName,
          departmentId: this.selectedDepartmentId,
          departmentName: this.user?.departmentName,
          roleId: this.selectedRoleId,
          roleName: this.user?.roleName
        };
        this.updateUserSubscription = this.userService.updateUser(this.id, editUser)
          .subscribe({
            next: (response) => {
              this.router.navigateByUrl('/admin/users')
            }
          });
      }
    }
    else {
      Object.keys(editUserForm.controls).forEach(field => {
        const control = editUserForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/users');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateUserSubscription?.unsubscribe();
  }
}
