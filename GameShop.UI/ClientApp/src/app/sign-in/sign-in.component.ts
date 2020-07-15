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

  constructor(private authService: AuthService , private router: Router) { }

  ngOnInit() {}

  login(): void {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully');
    } , error => {
      console.log(error);
    }, () => {
      const token = this.authService.loggedIn();
      this.authService.sendLoggedInStatus(token);
      const decodedToken = this.authService.decodedToken;
      console.log(decodedToken);
      this.authService.sendDecodedToken(decodedToken);
      console.log('TEST');
      this.router.navigate(['/home']);
    });

  }


  cancel(): void {
    console.log('canceled');
    this.router.navigate(['/home']);
  }

}
