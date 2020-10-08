import { ProductFromServer } from 'src/app/_models/ProductFromServer';
import { Product } from './../_models/Product';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { PaginatedResult } from '../_models/Pagination';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProductWithStockFromServer } from '../_models/ProductWithStockFromServer';


@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  createProduct(product: Product): Observable<any>  {
    return this.http.post(this.baseUrl + 'admin/create-product', product);
  }

  deletePhoto(productId: Number, id: Number): Observable<any>  {
    return this.http.delete(this.baseUrl + 'admin/product/' + productId + '/photos/' + id);
  }

  deleteProduct(id: Number): Observable<any>  {
    return this.http.delete(this.baseUrl + 'admin/delete-product/' + id);
  }

  editProduct(product: Product, id: Number): Observable<any>  {
    return this.http.post(this.baseUrl + 'admin/edit-product/' + id, product);
  }

  setMainPhoto(productId: Number, id: Number): Observable<any> {
    return this.http.post(this.baseUrl + 'admin/product/' + productId + '/photos/' + id + '/setMain', {});
  }

  getProducts(page?, itemsPerPage?): Observable<PaginatedResult<Array<ProductFromServer>>>  {
    const paginatedResult: PaginatedResult<Array<ProductFromServer>> = new PaginatedResult<Array<ProductFromServer>>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
        params =  params.append('pageNumber', page);
        params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get(this.baseUrl + 'admin/prodcuts-for-moderation', {observe: 'response', params})
            .pipe(
              map(response => {
              paginatedResult.result = <Array<ProductFromServer>>response.body;
              if (response.headers.get('Pagination') != null) {
                  paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
              }
              return paginatedResult;
            })
            );
  }

  getProductsWithStock(page?, itemsPerPage?): Observable<PaginatedResult<Array<ProductWithStockFromServer>>>  {
    const paginatedResult: PaginatedResult<Array<ProductWithStockFromServer>> = new PaginatedResult<Array<ProductWithStockFromServer>>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
        params =  params.append('pageNumber', page);
        params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get(this.baseUrl + 'admin/prodcuts-for-stock-moderation', {observe: 'response', params})
            .pipe(
              map(response => {
              paginatedResult.result = <Array<ProductWithStockFromServer>>response.body;
              if (response.headers.get('Pagination') != null) {
                  paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
              }
              return paginatedResult;
            })
            );
  }

  editStockForProduct(productId: number, stockQuantity: number) {
    return this.http.post(this.baseUrl + `admin/edit-product/${productId}/stock-quantity/${stockQuantity}`, {});
  }

  getProductForEdit(id: Number): Observable<any>  {
    return this.http.get(this.baseUrl + 'admin/product-for-edit/' + id);
  }

  getCategories(): Observable<any>  {
    return this.http.get(this.baseUrl + 'admin/available-categories');
  }

  getSubCategories(): Observable<any>  {
    return this.http.get(this.baseUrl + 'admin/available-subCategories');
  }

  getLanguages(): Observable<any>  {
    return this.http.get(this.baseUrl + 'admin/available-languages');
  }

  getUsersWithRoles(): Observable<any>  {
     return this.http.get(this.baseUrl + 'admin/users-with-roles');

  }

  updateUserRoles(user: User, roles: {}): Observable<any>  {
    return this.http.post(this.baseUrl + 'admin/edit-roles/' + user.userName, roles);
  }

}
