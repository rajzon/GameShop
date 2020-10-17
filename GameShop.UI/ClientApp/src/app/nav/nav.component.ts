import { MessagePopupService } from './../_services/message-popup.service';
import { AuthService } from './../_services/auth.service';
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

  constructor(private authService: AuthService , private router: Router, private messagePopup: MessagePopupService) {}

  ngOnInit() {
    this.subscription = this.authService.getLoggedInStatus().subscribe(x => {
      this.loggedIn = x;
      console.log('LoggedInStatus From nav bar passed by sign-in component' + this.loggedIn);
    });

    this.authService.sendLoggedInStatus(this.authService.loggedIn());

  }


  setAnonymousMode(): void {
    this.anonymousMode = true;
  }

  logout(): void {
    localStorage.removeItem('token');

    console.log(this.loggedIn);
    this.authService.sendLoggedInStatus(this.authService.loggedIn());

    console.log(this.loggedIn);
    const decodedToken = null;
    this.authService.sendDecodedToken(decodedToken);

    this.router.navigate(['/home']);
    this.messagePopup.displaySuccess('Logged out successfully');
  }



}
