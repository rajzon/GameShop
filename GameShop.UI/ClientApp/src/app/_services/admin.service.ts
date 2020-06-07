import { Product } from './../_models/Product';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';



@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'api/';

  constructor(private http: HttpClient) { }

  createProduct(product: Product) {
    return this.http.post(this.baseUrl + 'admin/create-product', product);
  }

  deleteProduct(id: Number) {
    const httpOpitons = {
      headers: new HttpHeaders({responseType: 'application/octet-stream'})};
    return this.http.delete(this.baseUrl + 'admin/delete-product/' + id);
  }

  editProduct(product: Product, id: Number) {
    return this.http.post(this.baseUrl + 'admin/edit-product/' + id, product);
  }
 

  getProducts() {
    return this.http.get(this.baseUrl + 'admin/prodcuts-for-moderation');
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
