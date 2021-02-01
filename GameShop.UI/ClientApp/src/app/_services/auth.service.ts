import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Subject, Observable} from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
   baseUrl = environment.baseUrl + 'auth/';
   jwtHelper = new JwtHelperService();
   decodedToken: any;


  private loggedInStatus = new Subject<any>();
  private tokenSubject = new Subject<any>();

  constructor(private http: HttpClient) { }

  login(model: any): Observable<any> {
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

  register(model: any): Observable<any> {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  public sendLoggedInStatus(loggedInStatus: boolean): void {
    this.loggedInStatus.next(loggedInStatus);
  }
  public getLoggedInStatus(): Observable<any> {
    return this.loggedInStatus.asObservable();
  }


  public sendDecodedToken(decodedToken: any): void {
    this.tokenSubject.next(decodedToken);
  }

  public activateHasRoleDirective(): Observable<any> {
    return this.tokenSubject.asObservable();
  }





}
