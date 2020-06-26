import { ProductForSearching } from './../_models/ProductForSearching';
import { PaginatedResult } from './../_models/Pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopSearchingService {
  baseUrl = 'api/';

constructor(private http: HttpClient) { }

  getProductsForSearching(page?, itemsPerPage?): Observable<PaginatedResult<Array<ProductForSearching>>> {
    const paginatedResult: PaginatedResult<Array<ProductForSearching>> = new PaginatedResult<Array<ProductForSearching>>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get(this.baseUrl + 'products', {observe: 'response', params})
        .pipe(
          map(response => {
            paginatedResult.result = <Array<ProductForSearching>>response.body;
            if (response.headers.get('Pagination') != null) {
              paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
            }
            return paginatedResult;
          })
        );
  }

  getProductForCard(id: number) {
    return this.http.get(this.baseUrl + 'products/' + id);
  }

}
