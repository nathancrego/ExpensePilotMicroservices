import { AfterViewInit, Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddRolesComponent } from '../add-roles/add-roles.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Roles } from '../models/roles.model';
import { MatTableDataSource } from '@angular/material/table';
import { RolesService } from '../services/roles.service';
import { Observable, Subscription } from 'rxjs';
import { EditRolesComponent } from '../edit-roles/edit-roles.component';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.css'
})
export class RolesListComponent implements OnInit, AfterViewInit, OnDestroy {

  //Point to remember - direct assignment of observable to Mattable doesn't work as it expects an array. Therefore we dont define observables in the declaration part.
  dataSource: MatTableDataSource<Roles> = new MatTableDataSource<Roles>();
  displayedColumns: string[] = ["id", "role", "action"];

  id: string | null = null;
  deleteRoleSubscription?: Subscription;

  readonly dialog = inject(MatDialog);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private rolesService: RolesService) { }

  //Adding Role by opening the Dialog box
  openAddDialog() {
    const adddialogRef = this.dialog.open(AddRolesComponent);
    adddialogRef.afterClosed().subscribe({
      next: (response) => {
        this.ngOnInit(); //refresh the table once the record is deleted
      }
    });
  }

  //load the roles
  ngOnInit(): void {
    this.rolesService.getAllRoles().subscribe({
      next: (roles) => {
        this.dataSource.data = roles; //Assign array directly to datasource
      },
      error: (err) => {
        console.error('Failed to fetch roles', err);
      }
    });
  }

  //once data is loaded, include the pagination and sorting
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  //Function to apply filter when data is entered
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  //function to delete a role record
  onDelete(): void {
    if (this.id) {
      //call roles service
      this.deleteRoleSubscription = this.rolesService.deleteRole(this.id)
        .subscribe({
          next:(response) =>
          {
            this.ngOnInit(); //refresh the table once the record is deleted
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.deleteRoleSubscription?.unsubscribe();
  }

}
