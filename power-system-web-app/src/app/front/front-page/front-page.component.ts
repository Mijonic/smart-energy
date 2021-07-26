import { ToastrService } from 'ngx-toastr';
import { AuthService } from './../../services/auth.service';

import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NavbarMessagingService } from 'app/services/navbar-messaging.service';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-front-page',
  templateUrl: './front-page.component.html',
  styleUrls: ['./front-page.component.css']
})
export class FrontPageComponent implements OnInit, OnDestroy {
  navbarMessagingSubscription!:Subscription;
  showLogin:boolean = false;
  showRegister:boolean = false;
  showReportOutage:boolean = false;
  @ViewChild('login') openBtn: ElementRef;

  constructor(private navbarMessaging:NavbarMessagingService, private auth:AuthService, private toastr:ToastrService) { }

  ngOnDestroy(): void {
    if(this.navbarMessagingSubscription)
      this.navbarMessagingSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.navbarMessagingSubscription = this.navbarMessaging.getMessage().subscribe( message => {
      if(message == "login")
      {
          this.showLoginForm();
      }else{
        this.showRegistrationForm();
      }

    });
    if(localStorage.length > 0)
    {
      this.toastr.warning("You are logged out now.","", {positionClass: 'toast-bottom-left'});
      this.auth.signOut();
    }
    
  }

  showLoginForm()
  { 
    this.showRegister = false;
    this.openBtn.nativeElement.click();
    this.showLogin = true;
    this.showReportOutage = false;
    
  }

  showRegistrationForm()
  { 
    this.showLogin = false; 
    this.showRegister = true;
    this.showReportOutage = false;
  }

  activateReportOutage()
  {
    this.showLogin = false; 
    this.showRegister = false;
    this.showReportOutage = true;
  }

}
