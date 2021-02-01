import { UserAddressInfoForEdition } from './../_models/UserAddressInfoForEdition';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { UserInfoAccToEdit } from '../_models/UserInfoAccToEdit';
import { UserAddressInfoForCreation } from '../_models/UserAddressInfoForCreation';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getUserForAccInfo(id: number): Observable<any> {
    return this.http.get(this.baseUrl + 'users/' + id);
  }

  editUserAccInfo(id: number, userInfoAccToEdit: UserInfoAccToEdit) {
    return this.http.post(this.baseUrl + 'users/' + id + '/edit-acc', userInfoAccToEdit);
  }

  createUserAddress(id: number, userAddressInfoForCreation: UserAddressInfoForCreation) {
    return this.http.post(this.baseUrl + 'users/' + id + '/create-address', userAddressInfoForCreation);
  }

  getUserAddresses(id: number) {
    return this.http.get(this.baseUrl + `address/user/${id}`);
  }

  deleteUserAddress(id: number, userId: number) {
    return this.http.delete(this.baseUrl + `address/delete/${id}/user/${userId}`);
  }

  editUserAddress(id: number, userId: number, addressInfo: UserAddressInfoForEdition) {
    return this.http.put(this.baseUrl + `users/${userId}/edit-address/${id}`, addressInfo);
  }

}
