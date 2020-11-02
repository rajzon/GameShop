import { MessagePopupService } from './../../_services/message-popup.service';
import { environment } from './../../../environments/environment';
import { ShopOrderingService } from './../../_services/shop-ordering.service';
import { Component, OnInit } from '@angular/core';
import { CustomerInfo } from 'src/app/_models/CustomerInfo';
import { Router } from '@angular/router';
import { AngularWaitBarrier } from 'blocking-proxy/built/lib/angular_wait_barrier';


@Component({
  selector: 'app-customer-info',
  templateUrl: './customer-info.component.html',
  styleUrls: ['./customer-info.component.css']
})
export class CustomerInfoComponent implements OnInit {

  model: CustomerInfo;
  nameMaxLength = environment.nameMaxLength;
  surNameMaxLength = environment.surNameMaxLength;
  addressMaxLength = environment.addressMaxLength;
  streetMaxLength = environment.streetMaxLength;
  postCodeMaxLength = environment.postCodeMaxLength;
  cityMaxLength = environment.cityMaxLength;

  constructor(private shopOrderingService: ShopOrderingService, private router: Router, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getCustomerInfo();
  }

  submitCustomerInfo() {
    const customerInfo = JSON.stringify(this.model);
    localStorage.setItem('customerInfo', customerInfo);
    this.router.navigate(['/checkout/payment']);
    // this.shopOrderingService.sendCustomerInfo(this.model);
  }

  getCustomerInfo() {
    const customerInfo = localStorage.getItem('customerInfo');
    this.model = JSON.parse(customerInfo);
    if (!this.model) {
      this.model = <CustomerInfo>{};
    }
  }

  cancel() {
    this.router.navigate(['/home']);
  }

}
