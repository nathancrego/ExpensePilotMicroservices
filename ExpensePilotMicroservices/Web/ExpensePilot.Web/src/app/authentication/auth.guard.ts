import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "./services/auth.service";

@Injectable({
  providedIn:'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      const user = this.authService.getUser();
      const roles = route.data['roles'] as Array<string>;


      if (user) {
        if (roles && roles.length > 0 && !roles.includes(user.role)) {
          // If user role doesn't match required roles, redirect to a forbidden page or home
          this.router.navigate(['/register']);
          return false;
        }
        return true; // User is authenticated and role matches (if specified)
      }

      // If not logged in, redirect to login
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }

}
