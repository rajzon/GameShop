import { BasketFromServer } from './../_models/BasketFromServer';
import { Component, OnInit } from '@angular/core';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { ProductForBasketFromServer } from '../_models/ProductForBasketFromServer';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basket: BasketFromServer;

  constructor(private  shopOrderingService: ShopOrderingService) { }

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


}
