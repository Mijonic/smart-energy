import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavigationModule } from './../navigation/navigation.module'
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontPageComponent } from './front-page/front-page.component';
import { ParticlesComponent } from './particles/particles.component';
import { NgParticlesModule } from 'ng-particles';
import { MatIconModule } from '@angular/material/icon';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ReportOutageComponent } from './report-outage/report-outage.component';
import {MatSelectModule} from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



@NgModule({
  declarations: [
    FrontPageComponent,
    ParticlesComponent,
    RegistrationComponent,
    LoginComponent,
    ReportOutageComponent

  ],
  exports:[
    FrontPageComponent,
    ParticlesComponent
  ],

  imports: [
    CommonModule,
    NgParticlesModule,
    MatIconModule,
    NavigationModule,
    MatSelectModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule
  ]
})
export class FrontModule { }
