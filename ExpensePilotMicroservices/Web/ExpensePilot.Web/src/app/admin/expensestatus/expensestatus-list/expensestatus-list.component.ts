import { AfterViewInit, Component, inject, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ExpenseStatus } from '../models/expensestatus.model';
import { Subscription } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ExpensestatusService } from '../services/expensestatus.service';

@Component({
  selector: 'app-expensestatus-list',
  templateUrl: './expensestatus-list.component.html',
  styleUrl: './expensestatus-list.component.css'
})
export class ExpensestatusListComponent implements OnInit, AfterViewInit, OnDestroy {

  //Point to remember - direct assignment of observable to Mattable doesn't work as it expects an array. Therefore we dont define observables in the declaration part.
  dataSource: MatTableDataSource<ExpenseStatus> = new MatTableDataSource<ExpenseStatus>();
  displayedColumns: string[] = ["id", "status", "action"];

  id: number | null = null;
  deleteExpenseStatusSubscription?: Subscription;

  readonly dialog = inject(MatDialog);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private expenseStatusService: ExpensestatusService) { }

  //load the roles
  ngOnInit(): void {
    this.expenseStatusService.getAllStatus().subscribe({
      next: (status) => {
        this.dataSource.data = status; //Assign array directly to datasource
      },
      error: (err) => {
        console.error('Failed to fetch Expense Status', err);
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
      this.deleteExpenseStatusSubscription = this.expenseStatusService.deleteStatus(this.id)
        .subscribe({
          next: (response) => {
            this.ngOnInit(); //refresh the table once the record is deleted
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.deleteExpenseStatusSubscription?.unsubscribe();
  }

}
