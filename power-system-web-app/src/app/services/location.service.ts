import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Location } from 'app/shared/models/location.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }
  
  getAllLocations():Observable<Location[]>{
    let requestUrl = environment.locationServerURL.concat("locations");
    return this.http.get<Location[]>(requestUrl);
  }
}
