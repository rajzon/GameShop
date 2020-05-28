import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class NegateAuthGuard implements CanActivate {
  private loggedIn: boolean;
  jwtHelper = new JwtHelperService();

  constructor(private router: Router) {}
  canActivate():  boolean  {
    const token = localStorage.getItem('token');
    this.loggedIn = !this.jwtHelper.isTokenExpired(token);
    console.log(this.loggedIn);
    if (!this.loggedIn) {
      return true;
    }

    this.router.navigate(['/home']);
    return false;
  }

}
