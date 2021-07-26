import { HttpEventType } from '@angular/common/http';
import { Component, ElementRef, Injector, OnInit, ViewChild} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { IMultimediaService } from 'app/shared/interfaces/multimedia-service';
import { FileUpload } from 'app/shared/models/file.model';
import { MultimediaAttachment } from 'app/shared/models/multimedia-attachment.model';
import * as fileSaver from 'file-saver';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-multimedia-attachments',
  templateUrl: './multimedia-attachments.component.html',
  styleUrls: ['./multimedia-attachments.component.css']
})
export class MultimediaAttachmentsComponent implements OnInit {
  documentId:number;
  @ViewChild("fileDropRef", { static: false }) fileDropEl: ElementRef;
  files: FileUpload[] = [];
  private multimediaService:IMultimediaService;
  multimediaAttachments:MultimediaAttachment[] = [];
  isLoading:boolean = true;

  constructor(private route:ActivatedRoute, private tabMessaging:TabMessagingService, injector:Injector,
    private toastr:ToastrService)
    {
      const serviceToken = route.snapshot.data['requiredService'];
      this.multimediaService = injector.get<IMultimediaService>(serviceToken);
      const wrId = this.route.snapshot.paramMap.get('id');
      if(wrId && wrId != "")
      {
        this.tabMessaging.showEdit(+wrId);
        this.documentId = +wrId;
      }
    }

  ngOnInit(): void {
    this.loadAttachments();
  }

  loadAttachments(){
    this.isLoading = true;
    this.multimediaService.getAttachments(this.documentId).subscribe(
      data => {
        this.multimediaAttachments = data;
        this.isLoading = false;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.loadAttachments();
        }else
        {
          this.toastr.error(`${error.error}`); 
          this.isLoading = false;
        }
      }
    )
  }

  onFileDropped($event: any[]) {
    this.prepareFilesList($event);
  }

  fileBrowseHandler(event:any) {
    let files = (<HTMLInputElement>event.target).files! ;
    this.prepareFilesList(Array.from(files));
  }

  deleteFile(file:FileUpload) {
    if (file.progress < 100 && !file.aborted) {
      return;
    }
    this.files = this.files.filter(x => x != file);
  }

  deleteFileAttachment(filename:string)
  {
    this.isLoading = true;
    this.multimediaService.deleteAttachment(filename, this.documentId).subscribe(
      data =>{
          this.loadAttachments();
          this.toastr.success("File deleted successfully.","", {positionClass: 'toast-bottom-left'});
          this.isLoading = false;
      },
      error =>{
        this.isLoading = false;
        if(error.error instanceof ProgressEvent)
        {
          this.toastr.error("Server is unreachable.","", {positionClass: 'toast-bottom-left'});
        }else
        {
          this.toastr.error(`${error.error}`); 
        }
      }
    )
  }

  uploadFiles(files:FileUpload[]){
    files.forEach(fileUpload => {
      this.multimediaService.uploadAttachment(fileUpload.file, this.documentId).subscribe( (event) => {
        if (event.type === HttpEventType.UploadProgress) {
          fileUpload.progress = Math.round(100 * event.loaded / event.total!);

        }else if(event.type === HttpEventType.Response && event.status == 200)
        {
          this.toastr.success(`File ${fileUpload.file.name} has been uploaded!`,"", {positionClass: 'toast-bottom-left'});
          this.loadAttachments();
        }
      }, (error) => {
        if(error.error instanceof ProgressEvent)
        {
          this.toastr.error("File can't be uploaded, server is unreachable","", {positionClass: 'toast-bottom-left'});
        }else
        {
          this.toastr.error(`Error occured uploading file ${fileUpload.file.name} ${error.error}`,"", {positionClass: 'toast-bottom-left'}); 
        }
        fileUpload.progress = 0;
        fileUpload.aborted = true;
        this.deleteFile(fileUpload);
      });

    });
  
  }

  prepareFilesList(files:  File[]) {
    let filesToUpload:FileUpload[] = [];
    for (const item of files) {
      let fileUpload = new FileUpload();
      fileUpload.file = item;
      fileUpload.progress = 0;
      this.files.push(fileUpload);
      filesToUpload.push(fileUpload);
    }
    this.fileDropEl.nativeElement.value = "";
    this.uploadFiles(filesToUpload);
  }

  downloadFile(filename:string) {
		this.multimediaService.downloadAttachment(this.documentId, filename).subscribe(
      (response: Blob) => { 
      fileSaver.saveAs(response, filename);
		},
     error =>
     {
        this.toastr.error(error.error);
     }
    )
  }

  formatBytes(bytes: number, decimals = 2) {
    if (bytes === 0) {
      return "0 Bytes";
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals;
    const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + " " + sizes[i];
  }


}

