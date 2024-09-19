import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../models/department.model';
import { AddDepartment } from '../models/add-department.model';
import { EditDepartment } from '../models/edit-department.model';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private baseurl = environment.apiUrls.admin

  //Injects and initializes the HttpClient
  constructor(private http: HttpClient) { }

  getAllDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.baseurl}/Department`)
  }

  getDepartmentById(id: number): Observable<Department> {
    return this.http.get<Department>(`${this.baseurl}/Department/${id}`);
  }

  createDepartment(model: AddDepartment): Observable<void> {
    return this.http.post<void>(`${this.baseurl}/Department`, model)
  }

  updateDepartment(id: number, editDepartment: EditDepartment): Observable<Department> {
    return this.http.put<Department>(`${this.baseurl}/Department/${id}`, editDepartment);
  }

  deleteDepartment(id: number): Observable<Department> {
    return this.http.delete<Department>(`${this.baseurl}/Department/${id}`);
  }
}
