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
  canActivate(): boolean {
    const customerInfoJSON = localStorage.getItem('customerInfo');
    const customerInfo = <CustomerInfo>JSON.parse(customerInfoJSON);
    if (!this.isCustomerInfoValid(customerInfo)) {
      this.messagePopup.displayError('Customer info do not contain all required fields');
      this.router.navigate(['home']);
      return false;
    }
    return true;

  }

  private isCustomerInfoValid(obj: CustomerInfo): boolean {
    if (!obj) {
      return false;
    }
    const requiredFields = ['name', 'surName', 'address', 'street', 'postCode' ];
    const checker = (arr, target) => target.every(v => arr.includes(v));
    if (!checker(Object.keys(obj), requiredFields) ||
          Object.entries(obj).filter(entry => entry[0] !== 'city').some(propVal => propVal[1].length === 0)){
          console.log(Object.entries(obj).filter(entry => entry[0] !== 'city'));
          console.log(Object.entries(obj).filter(entry => entry[0] !== 'city').some(propVal => console.log(propVal[1].length === 0)));
        return false;
    }
    return true;
  }

}
