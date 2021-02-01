import { BasketFromServer } from './../_models/BasketFromServer';
import { OrderInfo } from '../_models/OrderInfo';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopOrderingService {
  baseUrl = environment.baseUrl;

  private orderInfoSubject = new BehaviorSubject<OrderInfo>(<OrderInfo> { });
  constructor(private httpClient: HttpClient) { }

  public addStockToBasket(stockId: number, stockQty: number) {
    return this.httpClient.post(this.baseUrl + `basket/add-stock/${stockId}?stockQty=${stockQty}`, {}, {responseType: 'text'});
  }

  public getBasket(): Observable<BasketFromServer> {
    return this.httpClient.get<BasketFromServer>(this.baseUrl + 'basket/get-basket');
  }

  public deleteStockFromBasket(stockId: number) {
      return this.httpClient.delete(this.baseUrl + `basket/delete-stock/${stockId}`);
  }

  public sendCustomerInfo(orderInfo: OrderInfo) {
    this.orderInfoSubject.next(orderInfo);
  }

  public getCustomerInfo(): Observable<OrderInfo> {
    return this.orderInfoSubject.asObservable();
  }

  public chargePayment(stripeToken: string, orderInfo: OrderInfo) {
    return this.httpClient.post(this.baseUrl + 'payment/charge/',
            orderInfo, {headers: {'Stripe-Token': stripeToken}});
  }

  public synchronizeBasket() {
    return this.httpClient.post(this.baseUrl + 'basket/synchronize-basket', {});
  }

  public getDeliveryOpts() {
    return this.httpClient.get(this.baseUrl + 'deliveryOpts');
  }

  public clearBasket() {
    return this.httpClient.delete(this.baseUrl + 'basket/clear');
  }

}
