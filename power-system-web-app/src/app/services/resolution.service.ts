import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Resolution } from 'app/shared/models/resolution.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResolutionService {

  constructor(private http: HttpClient) { }
  
  getAllResolutions():Observable<Resolution[]>{
    let requestUrl = environment.serverURL.concat("resolution");
    return this.http.get<Resolution[]>(requestUrl);
  }

  getResolutionById(id:number):Observable<Resolution>{
    let requestUrl = environment.serverURL.concat(`resolution/${id}`);
    return this.http.get<Resolution>(requestUrl);
  }

  getResolutionIncident(incidentId:number):Observable<Resolution>{
    let requestUrl = environment.serverURL.concat(`resolution/incident/${incidentId}`);
    return this.http.get<Resolution>(requestUrl);
  }

  createNewResolution(resolution:Resolution):Observable<Resolution>{
    console.log(resolution)
    let requestUrl = environment.serverURL.concat("resolution");
    return this.http.post<Resolution>(requestUrl, resolution);
  }

  updateResolution(resolution: Resolution):Observable<Resolution>{
    let requestUrl = environment.serverURL.concat(`resolution/${resolution.id}`);
    return this.http.put<Resolution>(requestUrl, resolution);
  }

  deleteDevice(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`resolution/${id}`);
    return this.http.delete(requestUrl);
  }
}
