import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Message } from './models/message.model';

@Injectable({
  providedIn: 'root',
})
export class SimpleService {
  private endpoint = 'https://localhost:7004/api/hello';

  constructor(private _http: HttpClient) {}

  getHelloWorld(): Observable<Message> {
    return this._http.get<Message>(`${this.endpoint}`);
  }
}
