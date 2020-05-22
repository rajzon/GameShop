import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private loggedIn: boolean;

  constructor(private router: Router, private authService: AuthService) {}
  canActivate(next: ActivatedRouteSnapshot):  boolean  {
    const roles = next.data['roles'] as Array<string>;
    if (roles) {
      const match = this.authService.roleMatch(roles);
      if (match) {
        return true;
      } else {
        console.log('You do not have privlage to access that page');
        this.router.navigate(['home']);
      }

    }

    this.loggedIn = this.authService.loggedIn();
    console.log(this.loggedIn);
    if (this.loggedIn) {
      return true;
    }


    console.log('You are not allowed to get to that page');
    this.router.navigate(['/home']);
    return false;
  }

}
