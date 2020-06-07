import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopSearchingService {
  baseUrl = 'api/';

constructor(private http: HttpClient) { }

  getProductsForSearching() {
    return this.http.get(this.baseUrl + 'products');
  }

}
