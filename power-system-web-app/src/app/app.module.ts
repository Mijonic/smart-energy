import { SafetyDocumentService } from './services/safety-document.service';
import { IncidentService } from 'app/services/incident.service';
import { ErrorInterceptorService } from './services/error-interceptor.service';
import { AuthGuardService } from './auth/auth-guard.service';

import { WorkRequestService } from './services/work-request.service';
import { SettingsModule } from './settings/settings.module';
import { IncidentsModule } from './incidents/incidents.module';
import { CrewsModule } from './crews/crews.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardModule } from './dashboard/dashboard.module';
import { NavigationModule } from './navigation/navigation.module';
import {  NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, WORK_REQUEST_SERVICE_TOKEN, INCIDENT_SERVICE_TOKEN, SAFETY_DOCUMENTS_SERVICE_TOKEN } from './app-routing.module';
import { AppComponent } from './app.component';
import { DatePipe } from '@angular/common';
import { FrontModule } from './front/front.module';
import { DocumentsModule } from './documents/documents.module';
import { UsersModule } from './users/users.module';
import { NotificationsModule } from './notifications/notifications.module';
import { DevicesModule } from './devices/devices.module';
import { ToastrModule } from 'ngx-toastr';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { JwtModule } from "@auth0/angular-jwt";
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import {
  GoogleLoginProvider,
  FacebookLoginProvider
} from 'angularx-social-login';
import { HTTP_INTERCEPTORS } from '@angular/common/http';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavigationModule,
    FrontModule,
    DashboardModule,
    DocumentsModule,
    BrowserAnimationsModule,
    UsersModule,
    CrewsModule,
    IncidentsModule,
    SettingsModule,
    NotificationsModule,
    DevicesModule,
    SocialLoginModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains:['localhost:44372','localhost:44373', 'localhost:44374']
      }
    }),
  ],

  providers: [
    DatePipe,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
   /* {
      provide: INCIDENT_SERVICE_TOKEN,
      useClass: Define service here
   },*/
   {
      provide: WORK_REQUEST_SERVICE_TOKEN,
      useClass: WorkRequestService
   },
   {
    provide: INCIDENT_SERVICE_TOKEN,
    useClass: IncidentService
    },
    {
      provide: SAFETY_DOCUMENTS_SERVICE_TOKEN,
      useClass: SafetyDocumentService
      }
   /*Define other services here*/
   ,
   AuthGuardService,
   {
    provide: 'SocialAuthServiceConfig',
    useValue: {
      autoLogin: false,
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider(
            '985075988575-5dq1o575mkheantia2vblvskuav2d052.apps.googleusercontent.com'
          )
        },
        {
          id: FacebookLoginProvider.PROVIDER_ID,
          provider: new FacebookLoginProvider(
            '1645577359165702'
          )
        }
      ]
    } as SocialAuthServiceConfig,
  }    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
