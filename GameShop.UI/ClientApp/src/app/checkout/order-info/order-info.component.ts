import { DeliveryOptsFromServer } from './../../_models/DeliveryOptsFromServer';
import { MessagePopupService } from './../../_services/message-popup.service';
import { environment } from './../../../environments/environment';
import { Component, OnInit } from '@angular/core';
import { OrderInfo } from 'src/app/_models/OrderInfo';
import { Router } from '@angular/router';
import { BasketFromServer } from 'src/app/_models/BasketFromServer';
import { ShopOrderingService } from 'src/app/_services/shop-ordering.service';
import { IsOrderInfoValid } from 'src/app/_helpers/IsOrderInfoValid';


@Component({
  selector: 'app-customer-info',
  templateUrl: './order-info.component.html',
  styleUrls: ['./order-info.component.css']
})
export class OrderInfoComponent implements OnInit {
  editionAddressMode: boolean;
  model: OrderInfo;
  basket: BasketFromServer;
  deliveryOpts: DeliveryOptsFromServer[];

  constructor(private  shopOrderingService: ShopOrderingService, private router: Router, private messagePopup: MessagePopupService) { }

  ngOnInit(): void {
    this.getDeliveryOpts();
    this.getCustomerInfo();
    this.getBasket();
    console.log(this.model);
    
  }


  getBasket(): void {
    this.shopOrderingService.getBasket().subscribe((response: BasketFromServer) => {
      this.basket = response;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  getDeliveryOpts(): void {
    this.shopOrderingService.getDeliveryOpts().subscribe((respone : DeliveryOptsFromServer[]) => {
      this.deliveryOpts = respone;
    }, error => {
      this.messagePopup.displayError(error);
    }); 
  }


  getCustomerInfo(): void {
    const orderInfo = localStorage.getItem('orderInfo');
    this.model = JSON.parse(orderInfo);
    if (!this.model) {
      this.model = <OrderInfo>{};
    }
  }

  saveDeliveryOpt(): void {
    localStorage.setItem('orderInfo', JSON.stringify(this.model));
  }

  isOrderInfoDirty(): boolean {
    if(IsOrderInfoValid(this.model)) {
      return true;
    }
  }

  canceledOrSuccessfullAddressEdition(editionMode: boolean): void {
    this.editionAddressMode = editionMode;
    this.refreshComponent();
  }

  cancel(): void {
    this.router.navigate(['/home']);
  }

  goToPaySection(): void {
    this.router.navigate(['/checkout/payment']);
  }

  private refreshComponent(): void {
    this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/checkout/order-info']);
     });
  }

}
