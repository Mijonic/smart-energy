import { BrowserModule } from '@angular/platform-browser';

import { MultimediaModule } from './../multimedia/multimedia.module';
import { ChooseWorkRequestDialogComponent } from 'app/documents/dialogs/choose-work-request-dialog/choose-work-request-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { WorkPlansComponent } from './work-plans/work-plans.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule} from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule} from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSortModule } from '@angular/material/sort';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { WorkPlanComponent } from './work-plans/work-plan/work-plan.component';
import { MatTabsModule } from '@angular/material/tabs';
import { WorkPlanBasicInformationComponent } from './work-plans/work-plan-basic-information/work-plan-basic-information.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatDialogModule} from '@angular/material/dialog';
import { ChooseIncidentDialogComponent } from './dialogs/choose-incident-dialog/choose-incident-dialog.component';
import { StateChangeComponent } from './state-change/state-change.component';
import { WorkPlanStateChangesComponent } from './work-plans/work-plan-state-changes/work-plan-state-changes.component';
import { WorkPlanSwitchingInstructionsComponent } from './work-plans/work-plan-switching-instructions/work-plan-switching-instructions.component';
import { SwitchingInstructionComponent } from './switching-instruction/switching-instruction.component';
import { NewSwitchingInstructionDialogComponent } from './dialogs/new-switching-instruction-dialog/new-switching-instruction-dialog.component';
import { DocumentsMultimediaAttachmentComponent } from './documents-multimedia-attachment/documents-multimedia-attachment.component';
import { MatFileUploadModule } from 'angular-material-fileupload';
import { WorkPlanEquipmentComponent } from './work-plans/work-plan-equipment/work-plan-equipment.component';
import { ChooseEquipmentDialogComponent } from './dialogs/choose-equipment-dialog/choose-equipment-dialog.component';
import { SafetyDocumentsComponent } from './safety-documents/safety-documents/safety-documents.component';
import { SafetyDocumentComponent } from './safety-documents/safety-document/safety-document.component';
import { SafetyDocumentBasicInformationComponent } from './safety-documents/safety-document-basic-information/safety-document-basic-information.component';
import { SafetyDocumentStateChangesComponent } from './safety-documents/safety-document-state-changes/safety-document-state-changes.component';
import { SafetyDocumentEquipmentComponent } from './safety-documents/safety-document-equipment/safety-document-equipment.component';
import { SafetyDocumentChecklistComponent } from './safety-documents/safety-document-checklist/safety-document-checklist.component';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { WorkRequestsComponent } from './work-requests/work-requests.component';
import { WorkRequestComponent } from './work-requests/work-request/work-request.component';
import { WorkRequestBasicInformationComponent } from './work-requests/work-request/work-request-basic-information/work-request-basic-information.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { WorkRequestStateChangesComponent } from './work-requests/work-request/work-request-state-changes/work-request-state-changes.component';
import { WorkRequestEquipmentComponent } from './work-requests/work-request/work-request-equipment/work-request-equipment.component';


@NgModule({
  declarations: [
    WorkPlansComponent,
    WorkRequestsComponent,
    WorkPlanComponent,
    WorkPlanBasicInformationComponent,
    ChooseIncidentDialogComponent,
    ChooseWorkRequestDialogComponent,
    StateChangeComponent,
    WorkPlanStateChangesComponent,
    WorkPlanSwitchingInstructionsComponent,
    SwitchingInstructionComponent,
    NewSwitchingInstructionDialogComponent,
    DocumentsMultimediaAttachmentComponent,
    WorkPlanEquipmentComponent,
    ChooseEquipmentDialogComponent,
    SafetyDocumentsComponent,
    SafetyDocumentComponent,
    SafetyDocumentBasicInformationComponent,
    SafetyDocumentStateChangesComponent,
    SafetyDocumentEquipmentComponent,
    SafetyDocumentChecklistComponent,
    WorkRequestComponent,
    WorkRequestBasicInformationComponent,
    WorkRequestStateChangesComponent,
    WorkRequestEquipmentComponent,
  ],
  exports: [
    WorkPlansComponent,
    WorkRequestsComponent,
    WorkPlanComponent,
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    MatExpansionModule,
    MatRadioModule,
    RouterModule,
    MatProgressSpinnerModule,   
    MatTabsModule,
    MatDatepickerModule,     
    MatNativeDateModule,
    MatDialogModule,
    MatFileUploadModule,
    MatButtonToggleModule,
    MatCheckboxModule,
    MultimediaModule,
    BrowserModule,
    
  ]

})
export class DocumentsModule {
}
