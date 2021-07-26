import { MultimediaAttachment } from './../models/multimedia-attachment.model';
import { Observable } from "rxjs";
import { HttpEvent } from '@angular/common/http';

export interface IMultimediaService{
    downloadAttachment(documentId:number, filename:string): Observable<any>;
    getAttachments(documentId:number): Observable<MultimediaAttachment[]>;
    uploadAttachment(file: File, documentId:number): Observable<HttpEvent<any>>;
    deleteAttachment(filename:string, documentId:number): Observable<any>; 
}