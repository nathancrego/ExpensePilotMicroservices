import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { AuthUser } from '../models/authuser.model';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { environment } from '../../../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { RegistrationRequest } from '../models/registration-request.model';
import { RegistrationResponse } from '../models/registration-response.model';
import { jwtDecode } from "jwt-decode";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<AuthUser | undefined>(undefined);
  private baseurl = environment.apiUrls.auth;

  constructor(private http: HttpClient, private cookieService: CookieService) {
    // Restore the user from token on service initialization
    this.loadUserFromToken();
}

  //LOGIN
  login(request: LoginRequest): Observable<boolean> {
    return this.http.post<LoginResponse>(`${this.baseurl}/login`, { username: request.userName, password: request.password })
      .pipe(map((response: LoginResponse) => {
        if (response && response.token) {
          //Store JWT in localStorage or cookies as per your preference
          this.cookieService.set('Authorization', response.token, { path: '/' });
          this.setUserFromToken(response.token);
          return true;
        }
        return false;
      })
      );
  }

  //REGISTER
  register(request: RegistrationRequest): Observable<RegistrationResponse> {
    return this.http.post<RegistrationResponse>(`${this.baseurl}/register`, { fname: request.fName, lname: request.lName, email: request.email, phoneNumber:request.phoneNumber, password:request.password });
  }

  //OBSERVE USER
  user(): Observable<AuthUser | undefined> {
    return this.$user.asObservable();
  }

  //GET USER
  getUser(): AuthUser | undefined {
    const email = localStorage.getItem('user-email');
    const role = localStorage.getItem('user-role');
    if (email && role) {
      const user: AuthUser = {
        id: '',
        fName: '',
        lName: '',
        phoneNumber:'',
        email: email,
        role: role
      };
      return user;
    }
    return undefined;
  }


  //SET USER AFTER LOGIN
  private setUserFromToken(token: string): void {
    //You will need to decode the token and extract user info
    const payload = this.decodeToken(token)
    const user: AuthUser = {
      id: payload.id,
      fName: payload.fName,
      lName: payload.lName,
      phoneNumber: payload.phoneNumber,
      email: payload.email,
      role: payload.role
    };
    this.setUser(user);
  }

  // DECODE JWT TOKEN
  private decodeToken(token: string): any {
    // You can use a library like `jwt-decode` or do this manually
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));  // Base64 decode
  }

  //SET USER LOCALLY
  setUser(user: AuthUser): void {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-role', user.role);
  }

  // LOAD USER FROM TOKEN ON APP INIT
  private loadUserFromToken(): void {
    const token = this.cookieService.get('Authorization');
    if (token) {
      this.setUserFromToken(token);  // Decode token and set the user
    }
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
}
