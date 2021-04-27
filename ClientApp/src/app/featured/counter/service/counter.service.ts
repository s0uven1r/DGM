import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class CounterService {

  constructor( private http: HttpClient) {
  }
  validateUserData(): Observable<any>  {
    const loginUrl = "assets/counter.json";
    return  this.http
      .get(loginUrl);
  }
}
