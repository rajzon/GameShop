import { ShopOrderingService } from 'src/app/_services/shop-ordering.service';
import { MessagePopupService } from './../_services/message-popup.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  model: any = {};

  emailPattern = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$';

  userPasswordMaxLength: number = environment.userPasswordMaxLength;
  userPasswordMinLength: number = environment.userPasswordMinLength;


  constructor(private authService: AuthService , private router: Router, private messagePopup: MessagePopupService, private shopOrdering: ShopOrderingService) { }

  ngOnInit() {}

  login(): void {
    this.authService.login(this.model).subscribe(next => {
      this.messagePopup.displaySuccess('Logged in successfully');
    } , error => {
      this.messagePopup.displayError(error);
    }, () => {
      const token = this.authService.loggedIn();
      this.authService.sendLoggedInStatus(token);
      const decodedToken = this.authService.decodedToken;
      this.authService.sendDecodedToken(decodedToken);


      localStorage.removeItem('orderInfo');
      this.shopOrdering.clearBasket().subscribe(response => {
        this.messagePopup.displaySuccess('Basket cleared successfully');      
      }, error => {
        this.messagePopup.displayError(error);
      });

      this.router.navigate(['/home']); 
      
    });

  }


  cancel(): void {
    this.router.navigate(['/home']);
  }

}
