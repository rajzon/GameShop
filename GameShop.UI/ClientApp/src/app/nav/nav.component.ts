import { AuthService } from './../_services/auth.service';
import { LoggedInInfoService } from './../_services/loggedInInfo.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  anonymousMode: boolean;
  loggedIn: boolean;
  subscription: Subscription;

  constructor(private loggedInInfoService: LoggedInInfoService , private authService: AuthService , private router: Router) {}

  ngOnInit() {

    this.subscription = this.authService.getLoggedInStatus().subscribe(x => {
      this.loggedIn = x;
      console.log('LoggedInStatus From nav bar passed by sign-in component' + this.loggedIn);
    });

    this.authService.sendLoggedInStatus(this.authService.loggedIn());
  }


  setAnonymousMode() {
    this.anonymousMode = true;
  }

  logout() {
    localStorage.removeItem('token');
    this.authService.sendLoggedInStatus(this.authService.loggedIn());
    this.router.navigate(['/home']);
    console.log('Logged out');
  }



}
