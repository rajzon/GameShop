import { PaymentInfo } from './../_models/PaymentInfo';
import { BasketFromServer } from './../_models/BasketFromServer';
import { CustomerInfo } from './../_models/CustomerInfo';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopOrderingService {
  baseUrl = environment.baseUrl;

  private customerInfoSubject = new BehaviorSubject<CustomerInfo>(<CustomerInfo> { });
  constructor(private httpClient: HttpClient) { }

  addProductToBasket(productId: number, stockQty: number) {
    return this.httpClient.post(this.baseUrl + `basket/add-product/${productId}?stockQty=${stockQty}`, {});
  }

  getBasket(): Observable<BasketFromServer> {
    return this.httpClient.get<BasketFromServer>(this.baseUrl + 'basket/get-basket');
  }

  public sendCustomerInfo(customerInfo: CustomerInfo) {
    this.customerInfoSubject.next(customerInfo);
  }

  public getCustomerInfo(): Observable<CustomerInfo> {
    return this.customerInfoSubject.asObservable();
  }

  public chargePayment(stripeToken: string, customerInfo: CustomerInfo) {
    return this.httpClient.post(this.baseUrl + 'payment/charge/',
            customerInfo, {headers: {'Stripe-Token': stripeToken}});
  }

}
