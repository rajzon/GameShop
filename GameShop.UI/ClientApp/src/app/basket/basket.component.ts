import { Component, OnInit } from '@angular/core';
import { ShopOrderingService } from '../_services/shop-ordering.service';
import { ProductForBasketFromServer } from '../_models/ProductForBasketFromServer';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  products: ProductForBasketFromServer[];

  constructor(private  shopOrderingService: ShopOrderingService) { }

  ngOnInit() {
    this.getBasket();
  }


  getBasket() {
    this.shopOrderingService.getProductsForBasket().subscribe((response: ProductForBasketFromServer[]) => {
      console.log('get basket test successfull');
      console.log(response);
      this.products = response;
    }, error => {
      console.log(error);
    })
  }

}
