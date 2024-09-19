import { Component } from '@angular/core';
import { RegistrationRequest } from '../models/registration-request.model';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AuthUser } from '../models/authuser.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  model: RegistrationRequest;
  user?: AuthUser;
  errorMessage: string = '';
  successMessage: string = '';
  constructor(private authService: AuthService, private router: Router) {
    this.model = {
      fName: '',
      lName: '',
      email: '',
      phoneNumber: '',
      password:''
    }
  }

  onRegister(): void {
    if (!this.model.fName || !this.model.lName || !this.model.email || !this.model.phoneNumber || !this.model.password) {
      this.errorMessage = 'Name, Email, Phone and Password are required to register in to the application!';
      return;
    }

    if (!this.isValidEmail(this.model.email)) {
      this.errorMessage = 'Please enter a valid email address!';
      return;
    }

    //if (!this.isValidPhone(this.model.phoneNumber)) {
    //  this.errorMessage = 'Please enter a valid phone number!';
    //  return;
    //}


    this.authService.register(this.model).subscribe({
      next: (response) => {
        console.log('Registration response:', response); // <-- Add this for debugging
        // Clear any previous error messages
        this.errorMessage = '';
        // Set user in AuthService
        this.authService.setUser({
          email: response.email,
          role: '',
          id: response.id,
          fName: response.fName,
          lName: response.lName,
          phoneNumber: response.phoneNumber
        });
        // Display success message or navigate away
        this.successMessage = 'Registration successful! Redirecting...';

        // Optionally clear the model (form fields)
        this.model = {
          fName: '',
          lName: '',
          email: '',
          phoneNumber: '',
          password: ''
        };

        this.router.navigateByUrl('/policy');
      },
      error: (err) => {
        // Handle errors
        if (err.status === 401) {
          this.errorMessage = 'Invalid credentials. Please try again.';
        } else {
          this.errorMessage = err.error?.message || 'An error occurred. Please try again later.';
        }
      }
    });
  }

  // Helper method to validate email format
  private isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  //// Helper method to validate phone number format
  //private isValidPhone(phoneNumber: string): boolean {
  //  const phoneRegex = /^\d{10}$/;  // Simple check for a 10-digit number
  //  return phoneRegex.test(phoneNumber);
  //}

}
