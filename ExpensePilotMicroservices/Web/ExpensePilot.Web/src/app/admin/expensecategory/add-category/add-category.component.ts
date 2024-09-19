import { Component, OnDestroy } from '@angular/core';
import { AddExpenseCategory } from '../models/add-expensecategory.model';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent implements OnDestroy {

  category: AddExpenseCategory;
  private addExpenseCategorySubscription?: Subscription;

  constructor(private expensecategoryService: CategoryService, private router: Router) {
    this.category = {
      categoryName: '',
      lastUpdated: new Date()
    };
  }

  onFormSubmit(categoryForm: NgForm): void {
    if (categoryForm.valid) {
      this.addExpenseCategorySubscription = this.expensecategoryService.createExpenseCategory(this.category)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/expensecategory')
          }
        });
    }
    else {
      Object.keys(categoryForm.controls).forEach(field => {
        const control = categoryForm.control.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/admin/expensecategory');
  }

  ngOnDestroy(): void {
    this.addExpenseCategorySubscription?.unsubscribe();
  }
}
