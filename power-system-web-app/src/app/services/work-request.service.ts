import { WorkRequestStatistics } from './../shared/models/work-request-statistics.model';
import { StateChange } from './../shared/models/state-change.model';
import { IMultimediaService } from './../shared/interfaces/multimedia-service';
import { MultimediaAttachment } from './../shared/models/multimedia-attachment.model';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Device } from 'app/shared/models/device.model';
import { WorkRequest } from 'app/shared/models/work-request.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { WorkRequestList } from 'app/shared/models/work-request-list.model';

@Injectable({
  providedIn: 'root'
})
export class WorkRequestService implements IMultimediaService {

  constructor(private http: HttpClient) { }
  
  deleteAttachment(filename: string, documentId: number): Observable<any> {
    let requestUrl = environment.serverURL.concat(`work-requests/${documentId}/attachments/${filename}`);
    return this.http.delete(requestUrl);
  }
  
  createWorkRequest(workRequest:WorkRequest):Observable<WorkRequest>{
    let requestUrl = environment.serverURL.concat("work-requests");
    return this.http.post<WorkRequest>(requestUrl, workRequest);
  }

  getById(id:number):Observable<WorkRequest>{
    let requestUrl = environment.serverURL.concat(`work-requests/${id}`);
    return this.http.get<WorkRequest>(requestUrl);
  }

  getAll():Observable<WorkRequest[]>{
    let requestUrl = environment.serverURL.concat(`work-requests/all`);
    return this.http.get<WorkRequest[]>(requestUrl);
  }

  getWorkRequestStatistics(userId:number):Observable<WorkRequestStatistics>{
    let requestUrl = environment.serverURL.concat(`work-requests/statistics/${userId}`);
    return this.http.get<WorkRequestStatistics>(requestUrl);
  }


  getWorkrequestsPaged( page: number, perPage:number,sort?: string, order?: string, documentStatus?:string, searchParam?:string, documentOwner?:string):Observable<WorkRequestList>{
    let requestUrl = environment.serverURL.concat("work-requests");
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
    if(documentStatus)
      params = params.append('status', documentStatus);
    return this.http.get<WorkRequestList>(requestUrl, {params:params});
  }

  getWorkRequestDevices(id:number):Observable<Device[]>{
    let requestUrl = environment.serverURL.concat(`work-requests/${id}/devices`);
    return this.http.get<Device[]>(requestUrl);
  }

  updateWorkRequest(workRequest:WorkRequest):Observable<WorkRequest>{
    let requestUrl = environment.serverURL.concat(`work-requests/${workRequest.id}`);
    return this.http.put<WorkRequest>(requestUrl, workRequest);
  }

  deleteWorkRequest(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`work-requests/${id}`);
    return this.http.delete<WorkRequest>(requestUrl);
  }

  uploadAttachment(file: File, workRequestId:number): Observable<HttpEvent<any>> {
    let requestUrl = environment.serverURL.concat(`work-requests/${workRequestId}/attachments`);
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', requestUrl, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

  downloadAttachment(wrId:number, filename:string): Observable<any> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/attachments/${filename}`);
		return this.http.get(requestUrl, {responseType: 'blob'});
  }

  getAttachments(wrId:number): Observable<MultimediaAttachment[]> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/attachments`);
		return this.http.get<MultimediaAttachment[]>(requestUrl);
  }

  getStateChanges(wrId:number): Observable<StateChange[]> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/state-changes`);
		return this.http.get<StateChange[]>(requestUrl);
  }

  approveWorkRequest(wrId:number): Observable<WorkRequest> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/approve`);
		return this.http.put<WorkRequest>(requestUrl, {});
  }

  
  denyWorkRequest(wrId:number): Observable<WorkRequest> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/deny`);
		return this.http.put<WorkRequest>(requestUrl, {});
  }

  
  cancelWorkRequest(wrId:number): Observable<WorkRequest> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/cancel`);
		return this.http.put<WorkRequest>(requestUrl, {});
  }


}
