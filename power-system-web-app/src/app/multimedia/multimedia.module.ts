import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UploadProgressComponent } from './upload-progress/upload-progress.component';
import { DndDirective } from './dnd.directive';
import { MultimediaAttachmentsComponent } from './multimedia-attachments/multimedia-attachments.component';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [UploadProgressComponent, DndDirective, MultimediaAttachmentsComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],

  exports:[
    UploadProgressComponent,
    DndDirective
  ]
})
export class MultimediaModule { }
