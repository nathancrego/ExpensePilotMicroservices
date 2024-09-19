import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AddExpenseStatus } from '../models/add-expensestatus.model';
import { Observable } from 'rxjs';
import { ExpenseStatus } from '../models/expensestatus.model';
import { EditExpenseStatus } from '../models/edit-expensestatus.model';

@Injectable({
  providedIn: 'root'
})
export class ExpensestatusService {

  private baseurl = environment.apiUrls.admin

  constructor(private http: HttpClient) { }
  createExpenseStatus(addStatus: AddExpenseStatus): Observable<void> {
    return this.http.post<void>(`${this.baseurl}/ExpenseStatuses`, addStatus);
  }
  getAllStatus(): Observable<ExpenseStatus[]> {
    return this.http.get<ExpenseStatus[]>(`${this.baseurl}/ExpenseStatuses`);
  }
  getStatusById(id: number): Observable<ExpenseStatus> {
    return this.http.get<ExpenseStatus>(`${this.baseurl}/ExpenseStatuses/${id}`);
  }
  updateStatus(id: number, editStatus: EditExpenseStatus): Observable<ExpenseStatus> {
    return this.http.put<ExpenseStatus>(`${this.baseurl}/ExpenseStatuses/${id}`, editStatus);
  }
  deleteStatus(id: number): Observable<ExpenseStatus> {
    return this.http.delete<ExpenseStatus>(`${this.baseurl}/ExpenseStatuses/${id}`);
  }
}
