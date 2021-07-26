import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-documents-multimedia-attachment',
  templateUrl: './documents-multimedia-attachment.component.html',
  styleUrls: ['./documents-multimedia-attachment.component.css']
})
export class DocumentsMultimediaAttachmentComponent implements OnInit {
  @Input()
  uploadApiUrl:string;

  constructor() { }

  ngOnInit(): void {
  }

}
