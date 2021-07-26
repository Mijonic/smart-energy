import { CrewsList } from './../shared/models/crews-list.model';
import { Crew } from 'app/shared/models/crew.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CrewService {

  constructor(private http: HttpClient) { }

  getCrewsPaged(sort: string, order: string, page: number, perPage:number):Observable<CrewsList>{
    let requestUrl = environment.serverURL.concat("crews");
    let params = new HttpParams();
    params = params.append('sortBy', sort);
    params = params.append('direction', order);
    params = params.append('page', page.toString());
    params = params.append('perPage', perPage.toString());
    return this.http.get<CrewsList>(requestUrl, {params:params});
  }

  getAllCrews():Observable<Crew[]>{
    let requestUrl = environment.serverURL.concat("crews/all");
    return this.http.get<Crew[]>(requestUrl);
  }

  getCrewById(id:number):Observable<Crew>{
    let requestUrl = environment.serverURL.concat(`crews/${id}`);
    return this.http.get<Crew>(requestUrl);
  }

  createNewCrew(crew:Crew):Observable<Crew>{
    let requestUrl = environment.serverURL.concat("crews");
    return this.http.post<Crew>(requestUrl, crew);
  }

  updateCrew(crew:Crew):Observable<Crew>{
    let requestUrl = environment.serverURL.concat(`crews/${crew.id}`);
    return this.http.put<Crew>(requestUrl, crew);
  }

  deleteCrew(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`crews/${id}`);
    return this.http.delete(requestUrl);
  }
}
