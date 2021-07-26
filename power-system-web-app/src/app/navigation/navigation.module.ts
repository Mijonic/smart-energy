import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterLoginNavbarComponent } from './register-login-navbar/register-login-navbar.component';
import { MainNavbarComponent } from './main-navbar/main-navbar.component';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    RegisterLoginNavbarComponent,
    MainNavbarComponent
  ],
  exports:[
    RegisterLoginNavbarComponent,
    MainNavbarComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    RouterModule
  ]
})
export class NavigationModule { }
