import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoggedInInfoService {


  private subject = new Subject<any>();
  constructor(private route: ActivatedRoute) {}

  public sendLoggedInStatus(loggedInStatus: boolean) {
    this.subject.next(loggedInStatus);
  }
  public getLoggedInStatus(): Observable<any> {
    return this.subject.asObservable();
  }

}
