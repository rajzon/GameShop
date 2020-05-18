import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private loggedIn: boolean;

  constructor(private router: Router) {}
  canActivate():  boolean  {
    this.loggedIn = !!localStorage.getItem('token');
    console.log(this.loggedIn);
    if (this.loggedIn) {
      return true;
    }


    console.log('You are not allowed to get to that page');
    this.router.navigate(['/home']);
    return false;
  }

}
