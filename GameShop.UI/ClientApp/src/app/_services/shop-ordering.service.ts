import { PaymentInfo } from './../_models/PaymentInfo';
import { BasketFromServer } from './../_models/BasketFromServer';
import { CustomerInfo } from './../_models/CustomerInfo';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShopOrderingService {
  baseUrl = environment.baseUrl;

  private customerInfoSubject = new BehaviorSubject<CustomerInfo>(<CustomerInfo> { });
  constructor(private httpClient: HttpClient) { }

  addStockToBasket(stockId: number, stockQty: number) {
    return this.httpClient.post(this.baseUrl + `basket/add-stock/${stockId}?stockQty=${stockQty}`, {}, {responseType: 'text'});
  }

  getBasket(): Observable<BasketFromServer> {
    return this.httpClient.get<BasketFromServer>(this.baseUrl + 'basket/get-basket');
  }

  deleteStockFromBasket(stockId: number) {
      return this.httpClient.delete(this.baseUrl + `basket/delete-stock/${stockId}`);
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
