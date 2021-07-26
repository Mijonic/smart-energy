import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { GlobalSettingsComponent } from './global-settings/global-settings.component';
import { RouterModule } from '@angular/router';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { GlobalSettingsChangePasswordComponent } from './global-settings-change-password/global-settings-change-password.component';
import { GlobalSettingsStreetsPriorityComponent } from './global-settings-streets-priority/global-settings-streets-priority.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { GlobalSettingsNotificationsDocumentsComponent } from './global-settings-notifications-documents/global-settings-notifications-documents.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { GlobalSettingsResetDefaultComponent } from './global-settings-reset-default/global-settings-reset-default.component';
import { GlobalSettingsIconsComponent } from './global-settings-icons/global-settings-icons.component';




@NgModule({
  declarations: [
    EditProfileComponent,
    GlobalSettingsComponent,
    GlobalSettingsChangePasswordComponent,
    GlobalSettingsStreetsPriorityComponent,
    GlobalSettingsNotificationsDocumentsComponent,
    GlobalSettingsResetDefaultComponent,
    GlobalSettingsIconsComponent
  ],
  exports:[
    EditProfileComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    RouterModule,
    MatListModule,
    MatButtonToggleModule,
    MatIconModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatCardModule,
    FormsModule

  ]
})
export class SettingsModule { }
