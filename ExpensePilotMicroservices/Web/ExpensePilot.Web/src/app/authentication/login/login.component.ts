import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AuthUser } from '../models/authuser.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model: LoginRequest;
  user?: AuthUser;
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router, private cookieService: CookieService) {
    this.model = {
      userName: '',
      password: ''
    };
  }

  onFormSubmit(): void {
    if (!this.model.userName || !this.model.password) {
      this.errorMessage = 'Username and password are required!';
      return;
    }
    this.authService.login(this.model).subscribe({
      next: (response) => {
        //Token setting is handled in the auth service
        //Set user this method is typically called after login success
        this.authService.setUser({
          email: this.model.userName,
          role: this.user?.role||'',
          id: this.user?.id||'',  
          fName: this.user?.fName||'',
          lName: this.user?.lName||'',
          phoneNumber: this.user?.phoneNumber||''

        });
        //Redirect to homepage
        this.router.navigateByUrl('/dashboard');
      },
      error: (err) => {
        // Handle errors robustly
        if (err.status === 401) {
          this.errorMessage = 'Invalid credentials. Please try again.';
        } else {
          this.errorMessage = err.error?.message || 'An error occurred during login. Please try again later.';
        }
      }
    });
  }
}
