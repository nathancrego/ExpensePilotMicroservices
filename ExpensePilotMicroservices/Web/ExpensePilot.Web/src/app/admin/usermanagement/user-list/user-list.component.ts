import { AfterViewInit, Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { User } from '../models/user.model';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserService } from '../services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { AddUserComponent } from '../add-user/add-user.component';
import { EditUserComponent } from '../edit-user/edit-user.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit, AfterViewInit, OnDestroy {

  dataSource: MatTableDataSource<User> = new MatTableDataSource<User>();
  displayedColumns: string[] = ["id","userName", "fname", "lname", "managerName", "email", "departmentName", "roleName", "action"];

  id: string | null = null;
  deleteUserSubscription?: Subscription;

  readonly dialog = inject(MatDialog);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private userService:UserService) { }

  //Adding User by opening the Dialog box
  openAddDialog() {
    const adddialogRef = this.dialog.open(AddUserComponent);
    adddialogRef.afterClosed().subscribe({
      next: (response) => {
        this.ngOnInit(); //refresh the table once the record is deleted
      }
    });
  }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe({
      next: (users) => {
        this.dataSource.data = users; //Assign array directly to datasource
      },
      error: (err) => {
        console.error('Failed to fetch users', err);
      }
    });
  }

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
      this.deleteUserSubscription = this.userService.deleteUser(this.id)
        .subscribe({
          next: (response) => {
            this.ngOnInit(); //refresh the table once the record is deleted
          }
        });
    }
  }


  ngOnDestroy(): void {
    this.deleteUserSubscription?.unsubscribe();
  }

}
