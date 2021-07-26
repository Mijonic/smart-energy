import { NavbarMessagingService } from './../../services/navbar-messaging.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register-login-navbar',
  templateUrl: './register-login-navbar.component.html',
  styleUrls: ['./register-login-navbar.component.css']
})
export class RegisterLoginNavbarComponent implements OnInit {

  constructor(private navbarMessaging:NavbarMessagingService) { }

  ngOnInit(): void {
  }

  activateLogin()
  {
    this.navbarMessaging.activateLogin();
  }

  activateRegister()
  {
    this.navbarMessaging.activateRegister();
  }

}
