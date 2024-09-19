import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AddExpenseCategory } from '../models/add-expensecategory.model';
import { Observable } from 'rxjs';
import { ExpenseCategory } from '../models/expensecategory.model';
import { EditExpenseCategory } from '../models/edit-expensecategory.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private baseurl = environment.apiUrls.admin

  constructor(private http: HttpClient) { }

  createExpenseCategory(addCategory: AddExpenseCategory): Observable<void> {
    return this.http.post<void>(`${this.baseurl}/ExpenseCategories`, addCategory);
  }
  getAllCategory(): Observable<ExpenseCategory[]> {
    return this.http.get<ExpenseCategory[]>(`${this.baseurl}/ExpenseCategories`);
  }
  getCategoryById(id: number): Observable<ExpenseCategory> {
    return this.http.get<ExpenseCategory>(`${this.baseurl}/ExpenseCategories/${id}`);
  }
  updateCategory(id: number, editCategory: EditExpenseCategory): Observable<ExpenseCategory> {
    return this.http.put<ExpenseCategory>(`${this.baseurl}/ExpenseCategories/${id}`, editCategory);
  }
  deleteCategory(id: number): Observable<ExpenseCategory> {
    return this.http.delete<ExpenseCategory>(`${this.baseurl}/ExpenseCategories/${id}`);
  }

}
