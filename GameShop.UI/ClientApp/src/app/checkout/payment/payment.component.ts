import { MessagePopupService } from './../../_services/message-popup.service';
import { CustomerInfo } from './../../_models/CustomerInfo';
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
  customerInfo: CustomerInfo;

  constructor(private shopOrderingService: ShopOrderingService, private router: Router, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getCustomerInfo();
    console.log(this.customerInfo);
    this.initStripe();
  }

  getCustomerInfo() {
    this.shopOrderingService.getCustomerInfo().subscribe((response: CustomerInfo) => {
      this.customerInfo = response;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }


  private initStripe() {
    this.stripe = Stripe('pk_test_51HUcEHLCiATHwblhpuZfFCGoag0ENDpLXq8eUyTp7XsBb9dIG8uq6BEsVPEI9nEJmicxswyAftv5gvzNEXXwsoFq00tqnZbQ4z');
    const elements = this.stripe.elements();

    this.card = elements.create('card');
    this.card.mount(this.cardElement.nativeElement);

    this.card.addEventListener('change', ({ error }) => {
        this.cardErrors = error && error.message;
        console.log(this.cardErrors);
    });
  }

  async handleForm(e) {
    e.preventDefault();

    const { token, error } = await this.stripe.createToken(this.card);

    if (error) {
      const cardErrors = error.message;
      this.messagePopup.displayError(cardErrors);
    } else {
      console.log(token.id);
      this.shopOrderingService.chargePayment(token.id, this.customerInfo).subscribe(() => {
        this.messagePopup.displaySuccess('Successfull Payment');
        this.router.navigate(['/home']);
      }, error => {
        this.messagePopup.displayError(error);
      });
      console.log(token);
    }
  }

}
