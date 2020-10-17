import { MessagePopupService } from './../../_services/message-popup.service';
import { environment } from './../../../environments/environment';
import { ShopOrderingService } from './../../_services/shop-ordering.service';
import { Component, OnInit } from '@angular/core';
import { CustomerInfo } from 'src/app/_models/CustomerInfo';
import { Router } from '@angular/router';


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
    this.shopOrderingService.sendCustomerInfo(this.model);
  }

  getCustomerInfo() {
    this.shopOrderingService.getCustomerInfo().subscribe((response: CustomerInfo) => {
      this.model = response;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  cancel() {
    this.router.navigate(['/home']);
  }

}
