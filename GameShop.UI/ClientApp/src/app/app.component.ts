import { JwtHelperService } from '@auth0/angular-jwt';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'app';
  decodedToken;
  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService) {}
  ngOnInit() {
   const token = localStorage.getItem('token');
   if (token) {
    this.decodedToken = this.jwtHelper.decodeToken(token);
    console.log(this.decodedToken);
    this.authService.decodedToken = this.decodedToken;
    //this.authService.activateHasRoleDiractive();
    //this.authService.sendDecodedToken(this.authService.decodedToken);
   }
  }
}
