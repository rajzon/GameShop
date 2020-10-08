import { BasketFromServer } from './../_models/BasketFromServer';
import { Component, OnInit } from '@angular/core';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { ProductForBasketFromServer } from '../_models/ProductForBasketFromServer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basket: BasketFromServer;

  constructor(private  shopOrderingService: ShopOrderingService, private router: Router) { }

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


}
