import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AddUser } from '../models/add-user.model';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { EditUser } from '../models/edit-user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseurl = environment.apiUrls.admin

  constructor(private http: HttpClient) { }

  createUser(addUser: AddUser): Observable<void> {
    return this.http.post<void>(`${this.baseurl}/Users`, addUser);
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseurl}/Users`);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.baseurl}/Users/${id}`)
  }

  updateUser(id: string, editUser: EditUser): Observable<User> {
    return this.http.put<User>(`${this.baseurl}/Users/${id}`, editUser);
  }

  deleteUser(id: string): Observable<User> {
    return this.http.delete<User>(`${this.baseurl}/Users/${id}`)
  }
}
