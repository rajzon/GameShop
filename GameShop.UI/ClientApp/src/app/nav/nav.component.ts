import { ShopOrderingService } from 'src/app/_services/shop-ordering.service';
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

  constructor(private authService: AuthService , private router: Router, private messagePopup: MessagePopupService, private shopOrdering: ShopOrderingService) {}

  ngOnInit() {
    this.subscription = this.authService.getLoggedInStatus().subscribe(x => {
      this.loggedIn = x;
    });

    this.authService.sendLoggedInStatus(this.authService.loggedIn());

  }


  setAnonymousMode(): void {
    this.anonymousMode = true;
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('orderInfo');
    localStorage.removeItem('selectedAddressBookElId');

    this.authService.sendLoggedInStatus(this.authService.loggedIn());

    const decodedToken = null;
    this.authService.sendDecodedToken(decodedToken);

    this.shopOrdering.clearBasket().subscribe(response => {
      this.messagePopup.displaySuccess('Basket cleared successfully');
    }, error => {
      this.messagePopup.displayError(error);   
    });
    
    this.router.navigate(['/home']);
       
    this.messagePopup.displaySuccess('Logged out successfully');
  }



}
