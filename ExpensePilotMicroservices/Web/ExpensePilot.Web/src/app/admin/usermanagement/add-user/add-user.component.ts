import { Component, OnDestroy } from '@angular/core';
import { AddUser } from '../models/add-user.model';
import { catchError, Observable, Subscription } from 'rxjs';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { User } from '../models/user.model';
import { Roles } from '../../rolemanagement/models/roles.model';
import { RolesService } from '../../rolemanagement/services/roles.service';


@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent implements OnDestroy {


  user: AddUser;
  managers$?: Observable<User[]> | undefined;
  roles$?: Observable<Roles[]> | undefined;
  private addUserSubscription?: Subscription;

  constructor(private userService: UserService, private rolesService:RolesService, private router: Router) {
    this.user = {
      fname: '',
      lname: '',
      email: '',
      phoneNumber: '',
      managerId: null,
      role: {
        id: '',
        roleName: ''
      }
    };
    // Initialize observables in constructor
    this.managers$ = this.userService.getAllUsers().pipe(
      catchError(error => {
        console.error('Error loading managers:', error);
        return ([]);
      })
    );

    // Initialize observables in constructor
    this.roles$ = this.rolesService.getAllRoles().pipe(
      catchError(error => {
        console.error('Error loading roles:', error);
        return ([]);
      })
    );
  }

  

  onSubmit(addUserForm: NgForm): void {
    if (addUserForm.valid) {
      this.addUserSubscription = this.userService.createUser(this.user)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/users')
          },
          error: (error) => {
            console.error('Error in saving user', error);
          }
        });
    }
    else {
      console.error('Form not valid')
    }
  }

  ngOnDestroy(): void {
    this.addUserSubscription?.unsubscribe();
  }
}
