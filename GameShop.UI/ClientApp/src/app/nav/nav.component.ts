import { AuthService } from './../_services/auth.service';
import { LoggedInInfoService } from './../_services/loggedInInfo.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { isEmpty } from 'rxjs/operators';

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
    // this.subscription = this.authService.getDecodedToken().subscribe(x => {
    //   this.authService.decodedToken = x;
    //   console.log('DecodedToken From nav bar passed by sign-in component' + this.authService.decodedToken);
    // });

    // this.authService.sendDecodedToken(this.authService.decodedToken);


    this.subscription = this.authService.getLoggedInStatus().subscribe(x => {
      this.loggedIn = x;
      console.log('LoggedInStatus From nav bar passed by sign-in component' + this.loggedIn);
    });

    this.authService.sendLoggedInStatus(this.authService.loggedIn());
     //this.authService.sendDecodedToken(this.authService.decodedToken);
     //this.authService.activateHasRoleDiractive();
     

  }


  setAnonymousMode() {
    this.anonymousMode = true;
  }

  logout() {
    localStorage.removeItem('token');
    console.log(this.loggedIn);
    this.authService.sendLoggedInStatus(this.authService.loggedIn());
    console.log(this.loggedIn);
    const decodedToken = null;
    this.authService.sendDecodedToken(decodedToken);
    this.router.navigate(['/home']);
    console.log('Logged out');
  }



}
