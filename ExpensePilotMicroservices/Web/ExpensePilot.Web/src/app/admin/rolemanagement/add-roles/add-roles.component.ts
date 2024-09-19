import { Component, OnDestroy } from '@angular/core';
import { AddRoles } from '../models/add-roles.model';
import { Subscription } from 'rxjs';
import { RolesService } from '../services/roles.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-roles',
  templateUrl: './add-roles.component.html',
  styleUrl: './add-roles.component.css'
})
export class AddRolesComponent implements OnDestroy {

  roles: AddRoles;
  private addRoleSubscription?: Subscription;

  constructor(private rolesService: RolesService, private router: Router) {
    this.roles = {
      roleName: ''
    };
  }

  onSubmit(addRoleForm: NgForm): void {
    if (addRoleForm.valid) {
      this.addRoleSubscription = this.rolesService.createRole(this.roles)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/roles')
          }
        });
    }
  }


    ngOnDestroy(): void {
      this.addRoleSubscription?.unsubscribe();
    }



}
