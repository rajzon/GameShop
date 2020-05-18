import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class NegateAuthGuard implements CanActivate {
  private loggedIn: boolean;

  constructor(private router: Router) {}
  canActivate():  boolean  {
    this.loggedIn = !!localStorage.getItem('token');
    console.log(this.loggedIn);
    if (!this.loggedIn) {
      return true;
    }

    this.router.navigate(['/home']);
    return false;
  }

}
