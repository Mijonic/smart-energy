import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ToastrService } from 'ngx-toastr';
import { StateChange } from './../../../../shared/models/state-change.model';
import { WorkRequestService } from './../../../../services/work-request.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-work-request-state-changes',
  templateUrl: './work-request-state-changes.component.html',
  styleUrls: ['./work-request-state-changes.component.css']
})
export class WorkRequestStateChangesComponent implements OnInit {
  workRequestId:number;
  stateChanges:StateChange[];
  isLoading:boolean;
  constructor(private workReqService:WorkRequestService, private toastr:ToastrService, private route:ActivatedRoute, private tabMessaging:TabMessagingService) { }

  ngOnInit(): void {
    const wrId = this.route.snapshot.paramMap.get('id');
    if(wrId != null && wrId != '')
    {
      this.loadStateChanges(+wrId);
      this.tabMessaging.showEdit(+wrId);
      this.workRequestId = +wrId;
    }
      

  }

  approve()
  {
    this.workReqService.approveWorkRequest(this.workRequestId).subscribe(
      data =>{
        this.toastr.success("Work request approved","", {positionClass: 'toast-bottom-left'});
        this.loadStateChanges(this.workRequestId);
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
    this.workReqService.denyWorkRequest(this.workRequestId).subscribe(
      data =>{
        this.toastr.success("Work request denied.","", {positionClass: 'toast-bottom-left'});
        this.loadStateChanges(this.workRequestId);
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
    this.workReqService.cancelWorkRequest(this.workRequestId).subscribe(
      data =>{
        this.toastr.success("Work request cancelled","", {positionClass: 'toast-bottom-left'});
        this.loadStateChanges(this.workRequestId);
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
    this.workReqService.getStateChanges(id).subscribe(
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
