import { SafetyDocumentService } from 'app/services/safety-document.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { UserService } from 'app/services/user.service';
import { ValidationService } from 'app/services/validation.service';
import { StateChange } from 'app/shared/models/state-change.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-safety-document-state-changes',
  templateUrl: './safety-document-state-changes.component.html',
  styleUrls: ['./safety-document-state-changes.component.css']
})
export class SafetyDocumentStateChangesComponent implements OnInit {

  stateChanges:StateChange[];
  isLoading:boolean;
  safetyDocId:number;

  constructor(public dialog:MatDialog, private validation:ValidationService, private route:ActivatedRoute, private userService:UserService,
    private toastr:ToastrService, private router:Router, private safetyDocService:SafetyDocumentService, private tabMessaging:TabMessagingService) {
    
  
  }

  ngOnInit(): void {
    const safetyDocumentId = this.route.snapshot.paramMap.get('id');
    if(safetyDocumentId != null && safetyDocumentId != '')
    {
      this.loadStateChanges(+safetyDocumentId);
      this.tabMessaging.showEdit(+safetyDocumentId!);
      this.safetyDocId = +safetyDocumentId;
    }
  }

  approve()
  {
    this.safetyDocService.approveSafetyDocument(this.safetyDocId).subscribe(
      data =>{
        this.toastr.success("Safety document approved","", {positionClass: 'toast-bottom-left'});
        this.loadStateChanges(this.safetyDocId);
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.toastr.error("Server unreachable","", {positionClass: 'toast-bottom-left'});
        }else{
          this.toastr.error(error.error);
        }
      }
    )
  }


  deny()
  {
    this.safetyDocService.denySafetyDocument(this.safetyDocId).subscribe(
      data =>{
        this.toastr.success("Safety document denied.","", {positionClass: 'toast-bottom-left'});
        this.loadStateChanges(this.safetyDocId);
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.toastr.error("Server unreachable","", {positionClass: 'toast-bottom-left'});
        }else{
          this.toastr.error(error.error);
        }
      }
    )
  }


  cancel()
  {
    this.safetyDocService.cancelSafetyDocument(this.safetyDocId).subscribe(
      data =>{
        this.toastr.success("Safety document cancelled","", {positionClass: 'toast-bottom-left'});
        this.loadStateChanges(this.safetyDocId);
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.toastr.error("Server unreachable","", {positionClass: 'toast-bottom-left'});
        }else{
          this.toastr.error(error.error);
        }
      }
    )
  }

  loadStateChanges(id:number){
    this.isLoading = true;
    this.safetyDocService.getStateChanges(id).subscribe(
      data =>{
        this.stateChanges = data;
        this.isLoading = false;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.loadStateChanges(id);
        }else{
          this.toastr.error(error.error);
          this.isLoading = false;
        }

      }
    )
  }


}
