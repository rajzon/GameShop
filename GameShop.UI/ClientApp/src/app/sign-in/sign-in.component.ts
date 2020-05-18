import { LoggedInInfoService } from './../_services/loggedInInfo.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  model: any = {};
  // @Output() loggedInStatus = new EventEmitter();

  // private loggedInStatus: boolean;

  constructor(private authService: AuthService , private router: Router) { }

  ngOnInit() {
    // this.loggedInInfoService.loggedInStatusSet$.subscribe((loggedInStatus) => {
    //   console.log(loggedInStatus);
    // });

    // this.loggedInStatus = this.loggedInInfoService.loggedInInfo;
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully');
    } , error => {
      console.log(error);
    }, () => {
      const token = this.authService.loggedIn();
      this.authService.sendLoggedInStatus(token);
      this.router.navigate(['/home']);
    });

  }


  cancel() {
    console.log('canceled');
    this.router.navigate(['/home']);
  }

}
