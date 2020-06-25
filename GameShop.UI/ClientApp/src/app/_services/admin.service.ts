import { ProductFromServer } from 'src/app/_models/ProductFromServer';
import { Product } from './../_models/Product';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { PaginatedResult } from '../_models/Pagination';
import { map } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'api/';

  constructor(private http: HttpClient) { }

  createProduct(product: Product) {
    return this.http.post(this.baseUrl + 'admin/create-product', product);
  }

  deletePhoto(productId: Number, id: Number) {
    return this.http.delete(this.baseUrl + 'admin/product/' + productId + '/photos/' + id);
  }

  deleteProduct(id: Number) {
    const httpOpitons = {
      headers: new HttpHeaders({responseType: 'application/octet-stream'})};
    return this.http.delete(this.baseUrl + 'admin/delete-product/' + id);
  }

  editProduct(product: Product, id: Number) {
    return this.http.post(this.baseUrl + 'admin/edit-product/' + id, product);
  }

  setMainPhoto(productId: Number, id: Number) {
    return this.http.post(this.baseUrl + 'admin/product/' + productId + '/photos/' + id + '/setMain', {});
  }
 

  getProducts(page?, itemsPerPage?) {
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

  getProductForEdit(id: Number) {
    return this.http.get(this.baseUrl + 'admin/product-for-edit/' + id);
  }

  getCategories() {
    return this.http.get(this.baseUrl + 'admin/available-categories');
  }

  getSubCategories() {
    return this.http.get(this.baseUrl + 'admin/available-subCategories');
  }

  getLanguages() {
    return this.http.get(this.baseUrl + 'admin/available-languages');
  }

  getUsersWithRoles() {
     return this.http.get(this.baseUrl + 'admin/users-with-roles');

  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'admin/edit-roles/' + user.userName, roles);
  }

}
