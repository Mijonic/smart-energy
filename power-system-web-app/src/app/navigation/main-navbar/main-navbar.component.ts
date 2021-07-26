import { AuthService } from './../../services/auth.service';
import { UserService } from './../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { User } from 'app/shared/models/user.model';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.css']
})
export class MainNavbarComponent implements OnInit {

  constructor(private router:Router, private toastr:ToastrService, private userService:UserService, private auth:AuthService) { }
  user:User;
  @ViewChild("avatar") avatar:ElementRef;

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem("user")!);
    this.loadUserImage();
  }

  onLogout()
  {
    this.auth.signOut();
    this.toastr.info("You have logged out","", {positionClass: 'toast-bottom-left'});
    this.router.navigate(["/"]);
  }

  loadUserImage()
  {
    this.userService.getUserAvatar(this.user.id, this.user.imageURL).subscribe(
      data =>{
        const reader = new FileReader();
        reader.readAsDataURL(data);
        reader.onload = _event => {
            this.avatar.nativeElement.src = reader.result!.toString(); 

        };
      },
      error =>{

      if(error.error instanceof ProgressEvent)
      {
        this.loadUserImage();
      }
    }

    )
  }

}
