import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications.component';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import {ScrollTopModule} from 'primeng/scrolltop';
import {MatListModule} from '@angular/material/list';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { NotificationComponent } from './notification/notification.component';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    NotificationsComponent,
    NotificationComponent
  ],
  exports: [
    NotificationsComponent
  ],
  imports: [
    CommonModule,
    MessagesModule,
    MessageModule,
    ScrollTopModule,
    MatListModule,
    MatButtonToggleModule,
    MatIconModule
  ]
})
export class NotificationsModule { }
