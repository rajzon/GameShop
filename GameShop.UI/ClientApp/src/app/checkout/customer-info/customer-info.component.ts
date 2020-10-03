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

  constructor(private shopOrderingService: ShopOrderingService,private router: Router) { }

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
      console.log(error);
    });
  }

  cancel() {
    this.router.navigate(['/home']);
  }

}
