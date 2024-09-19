import { AfterViewInit, Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ExpenseCategory } from '../models/expensecategory.model';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit, AfterViewInit, OnDestroy {

  //Point to remember - direct assignment of observable to Mattable doesn't work as it expects an array. Therefore we dont define observables in the declaration part.
  dataSource: MatTableDataSource<ExpenseCategory> = new MatTableDataSource<ExpenseCategory>();
  displayedColumns: string[] = ["id", "category", "action"];

  id: number | null = null;
  deleteExpenseCategorySubscription?: Subscription;

  readonly dialog = inject(MatDialog);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private expenseCategoryService: CategoryService) { }

  //load the roles
  ngOnInit(): void {
    this.expenseCategoryService.getAllCategory().subscribe({
      next: (category) => {
        this.dataSource.data = category; //Assign array directly to datasource
      },
      error: (err) => {
        console.error('Failed to fetch Expense Categories', err);
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
      this.deleteExpenseCategorySubscription = this.expenseCategoryService.deleteCategory(this.id)
        .subscribe({
          next: (response) => {
            this.ngOnInit(); //refresh the table once the record is deleted
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.deleteExpenseCategorySubscription?.unsubscribe();
  }

}
