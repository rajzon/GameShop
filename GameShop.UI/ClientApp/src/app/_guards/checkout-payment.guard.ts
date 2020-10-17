import { MessagePopupService } from './../_services/message-popup.service';
import { ShopOrderingService } from './../_services/shop-ordering.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { BasketFromServer } from '../_models/BasketFromServer';
import { map, catchError } from 'rxjs/operators';
import { CustomerInfo } from '../_models/CustomerInfo';

@Injectable({
  providedIn: 'root'
})
export class CheckoutPaymentGuard implements CanActivate {

  constructor(private router: Router, private shopOrderingService: ShopOrderingService, private messagePopup: MessagePopupService) {}
  canActivate(): Observable<boolean> {
    return this.shopOrderingService.getCustomerInfo().pipe(
      map((response: CustomerInfo) => {
        if (Object.keys(response).length < 5) {
          this.messagePopup.displayError('Customer info do not contain all required fields');
          this.router.navigate(['home']);
          return false;
        }
        return true;
      })
    );

  }

}
