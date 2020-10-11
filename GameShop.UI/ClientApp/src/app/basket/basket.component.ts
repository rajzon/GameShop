import { BasketMissingStocksModalComponent } from './basket-missing-stocks-modal/basket-missing-stocks-modal.component';
import { BasketFromServer } from './../_models/BasketFromServer';
import { Component, OnInit } from '@angular/core';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { ProductForBasketFromServer } from '../_models/ProductForBasketFromServer';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NotEnoughStockInfoFromServer } from '../_models/NotEnoughStockInfoFromServer';
import { isDeepStrictEqual } from 'util';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basket: BasketFromServer;

  constructor(private  shopOrderingService: ShopOrderingService, private router: Router, private modalService: BsModalService) { }

  ngOnInit() {
    this.getBasket();
  }


  getBasket() {
    this.shopOrderingService.getBasket().subscribe((response: BasketFromServer) => {
      console.log('get basket test successfull');
      console.log(response);
      this.basket = response;
    }, error => {
      console.log(error);
    });
  }


  deleteStockFromBasket(stockId: number) {
    this.shopOrderingService.deleteStockFromBasket(stockId).subscribe(() => {
      console.log('successfully deleted Stock from Basket');
      this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/basket']);
       });
    }, error => {
      console.log(error);
    });
  }
  synchronizeBasket() {
    this.shopOrderingService.synchronizeBasket().subscribe(() => {
      this.router.navigateByUrl('/checkout/customer-info');
    }, error => {
      console.log(error);
     if (this.isArrayOfNotEnoughStockInfoFromServer(error)) {
      const initialState = {
        error
      };
      this.modalService.show(BasketMissingStocksModalComponent, {initialState});
    }
      console.log(error);

    });
  }

  private isArrayOfNotEnoughStockInfoFromServer(error: any): boolean {
    if (error instanceof Array) {
      if (error.every(x => (x as NotEnoughStockInfoFromServer).stockId !== undefined
          && (x as NotEnoughStockInfoFromServer).productName !== undefined
          && (x as NotEnoughStockInfoFromServer).availableStockQty !== undefined))
      {
        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  }


}
