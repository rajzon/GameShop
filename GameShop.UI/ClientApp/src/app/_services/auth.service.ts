import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Subject, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;


  private subject = new Subject<any>();
  private tokenSubject = new Subject<any>();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token' , user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    console.log(userRoles);
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  public sendLoggedInStatus(loggedInStatus: boolean) {
    this.subject.next(loggedInStatus);
  }
  public getLoggedInStatus(): Observable<any> {
    return this.subject.asObservable();
  }

  public sendDecodedToken(decodedToken: any) {
    this.tokenSubject.next(decodedToken);
  }

  public activateHasRoleDiractive(): Observable<any> {
    return this.tokenSubject.asObservable();
  }





}
