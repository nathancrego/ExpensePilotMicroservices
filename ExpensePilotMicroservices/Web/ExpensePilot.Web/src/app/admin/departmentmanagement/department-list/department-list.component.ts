import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Department } from '../models/department.model';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrl: './department-list.component.css'
})
export class DepartmentListComponent implements OnInit, AfterViewInit, OnDestroy {

  //Point to remember - direct assignment of observable to Mattable doesn't work as it expects an array. Therefore we dont define observables in the declaration part.
  dataSource: MatTableDataSource<Department> = new MatTableDataSource<Department>();
  displayedColumns: string[] = ["id", "department", "action"];

  id: number | null = null;
  deleteDepartmentSubscription?: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private departmentService: DepartmentService) { }

  //load the roles
  ngOnInit(): void {
    this.departmentService.getAllDepartments().subscribe({
      next: (department) => {
        this.dataSource.data = department; //Assign array directly to datasource
      },
      error: (err) => {
        console.error('Failed to fetch Departments', err);
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
      this.deleteDepartmentSubscription = this.departmentService.deleteDepartment(this.id)
        .subscribe({
          next: (response) => {
            this.ngOnInit(); //refresh the table once the record is deleted
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.deleteDepartmentSubscription?.unsubscribe();
  }

}
