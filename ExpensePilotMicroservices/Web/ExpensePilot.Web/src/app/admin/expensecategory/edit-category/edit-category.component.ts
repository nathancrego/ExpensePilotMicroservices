import { Component, OnDestroy, OnInit } from '@angular/core';
import { ExpenseCategory } from '../models/expensecategory.model';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { EditExpenseCategory } from '../models/edit-expensecategory.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css'
})
export class EditCategoryComponent implements OnInit, OnDestroy {

  categoryID: number | null = null;
  category?: ExpenseCategory;

  paramSubscription?: Subscription;
  routeSubscription?: Subscription;
  updateExpenseCategorySubscription?: Subscription;
  deleteExpenseCategorySubscription?: Subscription;

  constructor(private expensecategoryService: CategoryService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    //Sunscription to map the id and also convert id to string and then back to number
    this.routeSubscription = this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        const idParam = params.get('id');
        this.categoryID = idParam !== null ? Number(idParam) : null;

        //Fetch Expense Category from API
        if (this.categoryID !== null) {
          this.expensecategoryService.getCategoryById(this.categoryID)
            .subscribe({
              next: (response) => {
                this.category = response;
              },
              error: (err) => {
                console.error('Error fetching expense category:', err);
              }
            });
        } else {
          console.warn('No valid expense category ID found in route parameters.');
        }
      },
      error: (err) => {
        console.error('Error retrieving route parameters:', err);
      }
    });
  }

  onFormSubmit(editcategoryForm: NgForm): void {
    if (editcategoryForm.valid) {
      //Convert Model to request object
      if (this.category && this.categoryID) {
        var editCategory: EditExpenseCategory = {
          categoryName: this.category?.categoryName,
          lastUpdated: new Date()
        };
        this.updateExpenseCategorySubscription = this.expensecategoryService.updateCategory(this.categoryID, editCategory)
          .subscribe({
            next: (response) => {
              this.router.navigateByUrl('/admin/expensecategory')
            }
          });
      }
    }
    else {
      Object.keys(editcategoryForm.controls).forEach(field => {
        const control = editcategoryForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onDelete(): void {
    if (this.categoryID) {
      //Call service to delete department
      this.deleteExpenseCategorySubscription = this.expensecategoryService.deleteCategory(this.categoryID)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/expensecategory')
          }
        })
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/expensecategory');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateExpenseCategorySubscription?.unsubscribe();
    this.deleteExpenseCategorySubscription?.unsubscribe();
  }
}
