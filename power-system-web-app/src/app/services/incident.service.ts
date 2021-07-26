import { IMultimediaService } from './../shared/interfaces/multimedia-service';
import { IncidentMapDisplay } from './../shared/models/incident-map-display.model';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Incident } from 'app/shared/models/incident.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Device } from 'app/shared/models/device.model';
import { Crew } from 'app/shared/models/crew.model';
import { Call } from 'app/shared/models/call.model';
import { MultimediaAttachment } from 'app/shared/models/multimedia-attachment.model';
import { IncidentList } from 'app/shared/models/incident-list.model';
import { IncidentsStatistics } from 'app/shared/models/incident-statistics.mode';

@Injectable({
  providedIn: 'root'
})
export class IncidentService implements IMultimediaService {

  constructor(private http: HttpClient) { }
  downloadAttachment(documentId: number, filename: string): Observable<any> {
    let requestUrl = environment.serverURL.concat(`incidents/${documentId}/attachments/${filename}`);
		return this.http.get(requestUrl, {responseType: 'blob'});
  }

  getAttachments(documentId: number): Observable<MultimediaAttachment[]> {
    let requestUrl = environment.serverURL.concat(`incidents/${documentId}/attachments`);
		return this.http.get<MultimediaAttachment[]>(requestUrl);
  }

  uploadAttachment(file: File, documentId: number): Observable<HttpEvent<any>> {
    let requestUrl = environment.serverURL.concat(`incidents/${documentId}/attachments`);
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', requestUrl, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

  deleteAttachment(filename: string, documentId: number): Observable<any> {
    let requestUrl = environment.serverURL.concat(`incidents/${documentId}/attachments/${filename}`);
    return this.http.delete(requestUrl);
  }



  getAllIncidents():Observable<Incident[]>{
    let requestUrl = environment.serverURL.concat("incidents/all");
    return this.http.get<Incident[]>(requestUrl);
  }

  getIncidentsPaged( page: number, perPage:number,sort?: string, order?: string, filter?:string, searchParam?:string, documentOwner?:string):Observable<IncidentList>{
   
    let requestUrl = environment.serverURL.concat("incidents");
    let params = new HttpParams();

    if(sort)
      params = params.append('sortBy', sort);
    if(order)
      params = params.append('direction', order);

    params = params.append('page', page.toString());
    params = params.append('perPage', perPage.toString());

    if(searchParam)
      params = params.append('searchParam', searchParam);

    if(documentOwner)
      params = params.append('owner', documentOwner);

    if(filter)
      params = params.append('filter', filter);

    return this.http.get<IncidentList>(requestUrl, {params:params});
  }



  getIncidentById(id:number):Observable<Incident>{
    let requestUrl = environment.serverURL.concat(`incidents/${id}`);
    return this.http.get<Incident>(requestUrl);
  }

  createNewIncident(incident:Incident):Observable<Incident>{
    console.log(incident)
    let requestUrl = environment.serverURL.concat("incidents");
    return this.http.post<Incident>(requestUrl, incident);
  }

  updateIncident(incident: Incident):Observable<Incident>{
    let requestUrl = environment.serverURL.concat(`incidents/${incident.id}`);
    return this.http.put<Incident>(requestUrl, incident);
  }

  deleteIncident(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`incidents/${id}`);
    return this.http.delete(requestUrl);
  }

  
  getIncidentDevices(incidentId: number):Observable<Device[]>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/devices`);
    return this.http.get<Device[]>(requestUrl);
  }

  getUnrelatedDevices(incidentId: number):Observable<Device[]>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/unrelated-devices`);
    return this.http.get<Device[]>(requestUrl);
  }


  addDeviceToIncident(incidentId: number, deviceId: number):Observable<Incident>{
   
    let incident: Incident = new Incident();
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/device/${deviceId}`);
    return this.http.post<Incident>(requestUrl, incident);                  
  }


  addCrewToIncident(incidentId: number, crewId: number):Observable<Incident>{
   
    let incident: Incident = new Incident();
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/crew/${crewId}`);
    return this.http.put<Incident>(requestUrl, incident);                
  }


  assignIncidetToUser(incidentId: number, userId: number):Observable<Incident>{
   
    let incident: Incident = new Incident();
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/assign/${userId}`);
    return this.http.put<Incident>(requestUrl, incident);                  
  }


 


  getIncidentCrew(incidentId:number):Observable<Crew>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/crew`);
    return this.http.get<Crew>(requestUrl);
  }


  

  removeCrewFromIncidet(incidentId: number):Observable<Incident>{

    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/remove-crew`);
    return this.http.put<Incident>(requestUrl, "");  

  }



  removeDeviceFromIncident(incidentId: number, deviceId: number):Observable<Incident>{

    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/remove-device/device/${deviceId}`);
    return this.http.put<Incident>(requestUrl, "");  //////////// proveiti
  }


  getIncidentCalls(incidentId: number):Observable<Call[]>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/calls`);
    return this.http.get<Call[]>(requestUrl);
  }


  addIncidentCall(incidentId: number, newCall: Call):Observable<Call>{
    
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/calls`);
    return this.http.post<Call>(requestUrl, newCall);
  }


  getNumberOfAffectedConsumers(incidentId: number):Observable<number[]>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/affected-consumers`);
    return this.http.get<number[]>(requestUrl);
  }


  getNumberOfIncidentCalls(incidentId: number):Observable<number[]>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/calls-counter`);
    return this.http.get<number[]>(requestUrl);
  }




  getIncidentsStatistics(userId:number):Observable<IncidentsStatistics>{
    let requestUrl = environment.serverURL.concat(`incidents/statistics/${userId}`);
    return this.http.get<IncidentsStatistics>(requestUrl);
  }
 
 
  //done
  getIncidentLocation(incidentId:number):Observable<Location>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/location`);
    return this.http.get<Location>(requestUrl);
  }

  getUnassignedIncidents():Observable<Incident[]>{
    let requestUrl = environment.serverURL.concat(`incidents/unassigned`);
    return this.http.get<Incident[]>(requestUrl);
  }

  getUnresolvedIncidents():Observable<IncidentMapDisplay[]>{
    let requestUrl = environment.serverURL.concat(`incidents/unresolved`);
    return this.http.get<IncidentMapDisplay[]>(requestUrl);
  }
}
