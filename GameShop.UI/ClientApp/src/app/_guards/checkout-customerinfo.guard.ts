import { CustomerInfo } from './../_models/CustomerInfo';
import { MessagePopupService } from './../_services/message-popup.service';
import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable, Subject, of, empty } from 'rxjs';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { BasketFromServer } from '../_models/BasketFromServer';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CheckoutCustomerinfoGuard implements CanActivate {

  constructor(private router: Router, private shopOrderingService: ShopOrderingService, private messagePopup: MessagePopupService ) {}

  canActivate(): Observable<boolean> {
    return this.shopOrderingService.getBasket().pipe(
    map((response: BasketFromServer) => {
      if (response.basketPrice > 0 && response.basketProducts.length > 0) {
          return true;

      } else {
        this.router.navigate(['home']);
        return false;
      }
    }), catchError(err => {
      this.messagePopup.displayError(err);
      this.router.navigate(['home']);
      return of(false);
    }));

  }


}
