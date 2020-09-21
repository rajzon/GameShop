import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopOrderingService {
  baseUrl = environment.baseUrl;

  constructor(private httpClient: HttpClient) { }

  addProductToBasket(productId: number, stockQty: number) {
    return this.httpClient.post(this.baseUrl + `basket/add-product/${productId}?stockQty=${stockQty}`, {});
  }

  getProductsForBasket(){
    return this.httpClient.get(this.baseUrl + 'basket/get-basket');
  }

}
