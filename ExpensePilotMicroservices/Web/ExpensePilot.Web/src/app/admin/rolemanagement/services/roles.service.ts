import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AddRoles } from '../models/add-roles.model';
import { Observable } from 'rxjs';
import { Roles } from '../models/roles.model';
import { EditRoles } from '../models/edit-roles.model';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  private baseurl = environment.apiUrls.admin

  constructor(private http: HttpClient) { }

  createRole(addRole: AddRoles): Observable<void> {
    return this.http.post<void>(`${this.baseurl}/Roles`, addRole);
  }

  getAllRoles(): Observable<Roles[]> {
    return this.http.get<Roles[]>(`${this.baseurl}/Roles`);
  }

  getRoleById(id: string): Observable<Roles> {
    return this.http.get<Roles>(`${this.baseurl}/Roles/${id}`)
  }

  updateRole(id: string, editRole: EditRoles): Observable<Roles> {
    return this.http.put<Roles>(`${this.baseurl}/Roles/${id}`, editRole);
  }

  deleteRole(id: string): Observable<Roles> {
    return this.http.delete<Roles>(`${this.baseurl}/Roles/${id}`)
  }

}
