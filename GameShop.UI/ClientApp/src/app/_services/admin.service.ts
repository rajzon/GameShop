import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';



@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'api/';

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
     return this.http.get(this.baseUrl + 'admin/users-with-roles');

  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'admin/edit-roles/' + user.userName, roles);
  }

}
