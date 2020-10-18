import { MessagePopupService } from './../_services/message-popup.service';
import { BasketMissingStocksModalComponent } from './basket-missing-stocks-modal/basket-missing-stocks-modal.component';
import { BasketFromServer } from './../_models/BasketFromServer';
import { Component, OnInit } from '@angular/core';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NotEnoughStockInfoFromServer } from '../_models/NotEnoughStockInfoFromServer';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basket: BasketFromServer;


  constructor(private  shopOrderingService: ShopOrderingService, private router: Router, private modalService: BsModalService,
              private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getBasket();
  }


  getBasket() {
    this.shopOrderingService.getBasket().subscribe((response: BasketFromServer) => {
      this.basket = response;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }


  deleteStockFromBasket(stockId: number) {
    this.shopOrderingService.deleteStockFromBasket(stockId).subscribe(() => {
      this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/basket']);
       });
    }, error => {
      this.messagePopup.displayError(error);
    });
  }
  synchronizeBasket() {
    this.shopOrderingService.synchronizeBasket().subscribe(() => {
      this.router.navigateByUrl('/checkout/customer-info');
    }, error => {
     if (this.isArrayOfNotEnoughStockInfoFromServer(error)) {
      const initialState = {
        error
      };
      this.modalService.show(BasketMissingStocksModalComponent, {initialState});
    } else {
      this.messagePopup.displayError(error);
    }


    });
  }

  private isArrayOfNotEnoughStockInfoFromServer(error: any): boolean {
    if (error instanceof Array) {
      if (error.every(x => (x as NotEnoughStockInfoFromServer).stockId !== undefined
          && (x as NotEnoughStockInfoFromServer).productName !== undefined
          && (x as NotEnoughStockInfoFromServer).availableStockQty !== undefined)) {
        return true;
      }
        return false;
    }


    return false;

  }


}
