import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoggedInInfoService {
  // get loggedInInfo(): boolean {
  //   const token = localStorage.getItem('token');
  //   this._loggedInStatus = !!token;
  //   return this._loggedInStatus;
  // }

  private subject = new Subject<any>();
  // private _loggedInStatus: boolean;

  // private _loggedInStatusSet: Subject<any[]>;
  // public loggedInStatusSet$: Observable<any[]>;

constructor(private route: ActivatedRoute) {
  // this._loggedInStatusSet = new Subject<any[]>();
  // this.loggedInStatusSet$ = this._loggedInStatusSet.asObservable();

  // this.init();
}

public sendLoggedInStatus(loggedInStatus: boolean) {
  this.subject.next(loggedInStatus);
}
public getLoggedInStatus(): Observable<any> {
  return this.subject.asObservable();
}

// private init(){
//   this.route.data.subscribe((data) => {
//     if(data.hasOwnProperty('loggedInStatus')) {
//       this._loggedInStatusSet.next(data['loggedInStatus']);
//     }
//   });
// }

}
