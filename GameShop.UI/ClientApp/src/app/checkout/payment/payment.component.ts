import { MessagePopupService } from './../../_services/message-popup.service';
import { OrderInfo } from '../../_models/OrderInfo';
import { ShopOrderingService } from './../../_services/shop-ordering.service';

import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';

declare var Stripe: stripe.StripeStatic;
@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})


export class PaymentComponent implements OnInit {
  @ViewChild('cardElement', { static: true }) cardElement: ElementRef;

  stripe: any;
  card: any;
  cardErrors: any;
  confirmation: any;

  stockIds: number[];
  orderInfo: OrderInfo;

  constructor(private shopOrderingService: ShopOrderingService, private router: Router, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getCustomerInfo();
    this.initStripe();
  }

  getCustomerInfo() {
    const orderInfoJSON = localStorage.getItem('orderInfo');
    this.orderInfo = JSON.parse(orderInfoJSON);
    if (!this.orderInfo) {
      this.orderInfo = <OrderInfo>{};
    }
  }


  private initStripe() {
    this.stripe = Stripe('pk_test_51HUcEHLCiATHwblhpuZfFCGoag0ENDpLXq8eUyTp7XsBb9dIG8uq6BEsVPEI9nEJmicxswyAftv5gvzNEXXwsoFq00tqnZbQ4z');
    const elements = this.stripe.elements();

    this.card = elements.create('card');
    this.card.mount(this.cardElement.nativeElement);

    this.card.addEventListener('change', ({ error }) => {
        this.cardErrors = error && error.message;
    });
  }

  async handleForm(e) {
    e.preventDefault();

    const { token, error } = await this.stripe.createToken(this.card);

    if (error) {
      const cardErrors = error.message;
      this.messagePopup.displayError(cardErrors);
    } else {
      this.shopOrderingService.chargePayment(token.id, this.orderInfo).subscribe(() => {
        this.messagePopup.displaySuccess('Successfull Payment, order created');

        localStorage.removeItem('orderInfo');
        localStorage.removeItem('selectedAddressBookElId')
        this.router.navigate(['/home']);
      }, error => {
        this.messagePopup.displayError(error);
      });
    }
  }

}
