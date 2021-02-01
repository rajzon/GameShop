import { MessagePopupService } from '../_services/message-popup.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, of, } from 'rxjs';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { map, catchError } from 'rxjs/operators';
import { BasketMissingStocksModalComponent } from '../basket/basket-missing-stocks-modal/basket-missing-stocks-modal.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { IsArrayOfNotEnoughStockInfoFromServer } from '../_helpers/isArrayOfNotEnoughStockInfoFromServer';

@Injectable({
  providedIn: 'root'
})
export class CheckoutOrderinfoGuard implements CanActivate {

  constructor(private router: Router, private shopOrderingService: ShopOrderingService,
        private messagePopup: MessagePopupService, private modalService: BsModalService ) {}

  canActivate(): Observable<boolean> {
    return this.shopOrderingService.synchronizeBasket().pipe(
      map(res => {
          return true;
      }), catchError(error => {
        console.log(error);
        if (IsArrayOfNotEnoughStockInfoFromServer(error)) {
          const initialState = {
            error
          };
          this.router.navigate(['basket']);
          this.modalService.show(BasketMissingStocksModalComponent, {initialState});
        } else {
          this.router.navigate(['home']);
          this.messagePopup.displayError(error);
        }
        return of(false);
      })
    );
  }

}
