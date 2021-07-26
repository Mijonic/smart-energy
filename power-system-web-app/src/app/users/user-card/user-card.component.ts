import { AuthGuardService } from './../../auth/auth-guard.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../services/user.service';
import { DatePipe } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { Location } from "app/shared/models/location.model";

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  @Input()
  user!:User;
  @Output() reload: EventEmitter<any> = new EventEmitter();
  @ViewChild("avatar") avatar:ElementRef;
  isLoadingImage:boolean = true;

  constructor(public datePipe:DatePipe, private userService:UserService, private toastr:ToastrService, public _authGuard:AuthGuardService) { }

  ngOnInit(): void {
    if(this.user.imageURL)
    {
      this.loadUserImage();
    }
    else
      this.isLoadingImage = false;
  }

  getDateForDisplay(date:Date)
  {
    return this.datePipe.transform(date, 'dd-MM-yyyy');

  }

  loadUserImage()
  {
    this.userService.getUserAvatar(this.user.id, this.user.imageURL).subscribe(
      data =>{
        this.isLoadingImage = false;
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
      }else
      {
        this.toastr.error(error.error);
        this.isLoadingImage = false;
      }
    }

    )
  }

  getUserDisplay()
  {
    return (`${this.user.name} ${this.user.lastname}`);
  }

  getUserTypeDisplay(type:string){
    if(type === 'CREW_MEMBER')
      return 'Crew member';

    if(type === 'DISPATCHER')
      return 'Disptacher';

    if(type === 'WORKER')
      return 'Worker';

    if(type === 'ADMIN')
      return 'Admin';

    if(type === 'CONSUMER')
      return 'Consumer';
    
    return "Unknown";
  }

  getLocationDisplayString(location:Location)
  {
    return `${location.street}, ${location.city}`;
  }

  approveUser(id:number){
    this.userService.approveUser(id).subscribe(
      data =>
        {
          this.toastr.success("User approved.","", {positionClass: 'toast-bottom-left'});
          this.reload.emit();
        },
      error =>{
        this.toastr.error(error.error);
        this.reload.emit();
      }
      );
  }

  denyUser(id:number){
    this.userService.denyUser(id).subscribe(
      data =>
        {
          this.toastr.success("User denied.","", {positionClass: 'toast-bottom-left'});
          this.reload.emit();
        },
      error =>{
        this.toastr.error(error.error);
        this.reload.emit();
      }
      );
  }

}
