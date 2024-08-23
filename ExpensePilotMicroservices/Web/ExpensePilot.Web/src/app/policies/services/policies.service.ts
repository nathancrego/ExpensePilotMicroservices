import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AddPolicy } from '../model/add-policy.model';
import { Observable } from 'rxjs';
import { Policy } from '../model/policy.model';
import { EditPolicy } from '../model/edit-policy.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PoliciesService {

  //assign a variable to the apiurl for policies
  private baseurl = environment.apiUrls.policies
  constructor(private http: HttpClient) { }

  createPolicy(addPolicy: AddPolicy): Observable<void>
  {
    return this.http.post<void>(`${this.baseurl}/add`, addPolicy);
  }

  getAllPolicies(): Observable<Policy[]>
  {
    return this.http.get<Policy[]>(`${this.baseurl}`);
  }

  getPolicyById(id: number): Observable<Policy> {
    return this.http.get<Policy>(`${this.baseurl}/${id}`);
  }

  updatePolicy(id: number, editPolicy: EditPolicy): Observable<Policy> {
    return this.http.put<Policy>(`${this.baseurl}/${id}`, editPolicy);
  }

  deletePolicy(id: number): Observable<Policy> {
    return this.http.delete<Policy>(`${this.baseurl}/${id}`);
  }
}
