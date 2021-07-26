import { IMultimediaService } from './../shared/interfaces/multimedia-service';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Checklist } from 'app/shared/models/checklist.model';
import { SafetyDocument } from 'app/shared/models/safety-document.model';
import { StateChange } from 'app/shared/models/state-change.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { MultimediaAttachment } from 'app/shared/models/multimedia-attachment.model';
import { Device } from 'app/shared/models/device.model';

@Injectable({
  providedIn: 'root'
})
export class SafetyDocumentService implements IMultimediaService{

  constructor(private http: HttpClient) { }

  downloadAttachment(documentId: number, filename: string): Observable<any> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${documentId}/attachments/${filename}`);
		return this.http.get(requestUrl, {responseType: 'blob'});
  }

  getAttachments(documentId: number): Observable<MultimediaAttachment[]> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${documentId}/attachments`);
		return this.http.get<MultimediaAttachment[]>(requestUrl);
  }

  uploadAttachment(file: File, documentId: number): Observable<HttpEvent<any>> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${documentId}/attachments`);
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', requestUrl, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

  getSafetyDocumentDevices(id:number):Observable<Device[]>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${id}/devices`);
    return this.http.get<Device[]>(requestUrl);
  }


  deleteAttachment(filename: string, documentId: number): Observable<any> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${documentId}/attachments/${filename}`);
    return this.http.delete(requestUrl);
  }

  
  getAllSafetyDocuments():Observable<SafetyDocument[]>{
    let requestUrl = environment.serverURL.concat("safety-documents/all");
    return this.http.get<SafetyDocument[]>(requestUrl);
  }

  getAllMineSafetyDocuments(documentOwner?: string):Observable<SafetyDocument[]>{
   
    let requestUrl = environment.serverURL.concat("safety-documents");
    let params = new HttpParams();

    if(documentOwner)
     params = params.append('owner', documentOwner);

    return this.http.get<SafetyDocument[]>(requestUrl, {params:params});
  }

  getSafetyDocumentById(id:number):Observable<SafetyDocument>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${id}`);
    return this.http.get<SafetyDocument>(requestUrl);
  }

  createNewSafetyDocument(safetyDocument:SafetyDocument):Observable<SafetyDocument>{
    
    let requestUrl = environment.serverURL.concat("safety-documents");
    console.log("slanje ka serveru")
    console.log(requestUrl);
    console.log(safetyDocument);
    return this.http.post<SafetyDocument>(requestUrl, safetyDocument);
  }

  updateSafetyDocument(safetyDocument: SafetyDocument):Observable<SafetyDocument>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${safetyDocument.id}`);
    return this.http.put<SafetyDocument>(requestUrl, safetyDocument);
  }

  updateSafetyDocumentChecklist(checklist: Checklist):Observable<Checklist>{
    console.log("U metodi serivsa");
    console.log(checklist)
    let requestUrl = environment.serverURL.concat(`safety-documents/${checklist.safetyDocumentId}/checklist`);
    return this.http.put<Checklist>(requestUrl, checklist);
  }

  

  getCrewForSafetyDocument(id:number):Observable<SafetyDocument>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${id}/crew`);
    return this.http.get<SafetyDocument>(requestUrl);
  }

  getStateChanges(wrId:number): Observable<StateChange[]> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/state-changes`);
		return this.http.get<StateChange[]>(requestUrl);
  }

  approveSafetyDocument(wrId:number): Observable<SafetyDocument> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/approve`);
		return this.http.put<SafetyDocument>(requestUrl, {});
  }

  
  denySafetyDocument(wrId:number): Observable<SafetyDocument> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/deny`);
		return this.http.put<SafetyDocument>(requestUrl, {});
  }

  
  cancelSafetyDocument(wrId:number): Observable<SafetyDocument> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/cancel`);
		return this.http.put<SafetyDocument>(requestUrl, {});
  }

 
}
