import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SimpleService {
  private endpoint = 'hello';

  constructor(private _http: HttpClient) {}

  getHelloWorld(): Observable<string> {
    return this._http.get<string>(`${this.endpoint}`);
  }
}
