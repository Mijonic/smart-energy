import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Consumer } from 'app/shared/models/consumer.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConsumerService {

  constructor(private http: HttpClient) { }

 
  GetConsumers():Observable<Consumer[]>{
    let requestUrl = environment.serverURL.concat("consumers");
    return this.http.get<Consumer[]>(requestUrl);
  }
}
