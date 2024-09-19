import { Component, Inject, OnInit } from '@angular/core';
import { Roles } from '../models/roles.model';
import { Subscription } from 'rxjs';
import { RolesService } from '../services/roles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { EditRoles } from '../models/edit-roles.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-roles',
  templateUrl: './edit-roles.component.html',
  styleUrl: './edit-roles.component.css'
})
export class EditRolesComponent implements OnInit {
  id: string | null = null;
  role?: Roles;

  paramSubscription?: Subscription;
  routeSubscription?: Subscription;
  updateRoleSubscription?: Subscription;

  constructor(private rolesService: RolesService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.rolesService.getRoleById(this.id)
            .subscribe({
              next: (respone) => {
                this.role = respone;
              }
            })
        }
      }
    });
  }

  onFormSubmit(): void {
    if (this.id && this.role) {
      var editRole: EditRoles = {
        roleName: this.role?.roleName
      };
      this.updateRoleSubscription = this.rolesService.updateRole(this.id, editRole)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/roles')
          }
        });
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/admin/roles');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateRoleSubscription?.unsubscribe();
  }

}
