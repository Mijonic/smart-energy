import { TabMessagingService } from './../../../../services/tab-messaging.service';
import { IncidentService } from './../../../../services/incident.service';
import { ToastrService } from 'ngx-toastr';
import { WorkRequestService } from './../../../../services/work-request.service';
import { UserService } from './../../../../services/user.service';
import { ValidationService } from './../../../../services/validation.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ChooseIncidentDialogComponent } from 'app/documents/dialogs/choose-incident-dialog/choose-incident-dialog.component';
import { User } from 'app/shared/models/user.model';
import { WorkRequest } from 'app/shared/models/work-request.model';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-work-request-basic-information',
  templateUrl: './work-request-basic-information.component.html',
  styleUrls: ['./work-request-basic-information.component.css']
})
export class WorkRequestBasicInformationComponent implements OnInit {
  documentTypes = [
                   {display:'Planned work', value:'PLANNED'},
                   {display:'Unplanned work', value:'UNPLANNED'},
                  ];
  user:User = new User();
  workRequest:WorkRequest = new WorkRequest();
  workRequestForm = new FormGroup({
    documentType: new FormControl('', [Validators.required]),
    startDate: new FormControl('', [Validators.required]),
    endDate: new FormControl('', Validators.required),
    purpose: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    note: new FormControl('', [Validators.maxLength(100)]),
    details: new FormControl('', [Validators.maxLength(100)]),
    companyName: new FormControl('', [Validators.maxLength(50)]),
    phone: new FormControl('', [Validators.maxLength(30)]),
    incident: new FormControl("", Validators.required),
    emergency:new FormControl(false),
    createdOn: new FormControl({value: new Date(), disabled: true}),
    status: new FormControl({value:'DRAFT', disabled:true}),
    createdBy: new FormControl({value:'', disabled:true}),

  }, {validators:this.logicalDates});
  isNew = true;
  isLoading:boolean = false;
  workReqId:number;

  constructor(public dialog:MatDialog, private validation:ValidationService, private route:ActivatedRoute, private userService:UserService, 
    private workRequestService:WorkRequestService, private toastr:ToastrService, private router:Router, private incidentService:IncidentService,
    private tabMessaging:TabMessagingService)
     
    {
      
    }

  ngOnInit(): void {
    const wrId = this.route.snapshot.paramMap.get('id');
      if(wrId && wrId != "")
      {
        this.tabMessaging.showEdit(+wrId);
        this.isNew = false;
        this.workReqId = +wrId;
        this.loadWorkRequest(this.workReqId);
      }else
      {
        this.user = JSON.parse(localStorage.getItem("user")!);
        this.workRequestForm.controls['createdBy'].setValue(`${this.user.name} ${this.user.lastname}`);
      }
    
  }

  loadWorkRequest(id:number)
  {
    this.isLoading = true;
      this.workRequestService.getById(id).subscribe(
        data =>{
          this.isLoading = false;
          this.workRequest = data;
          this.populateControls(this.workRequest);
        } ,
        error =>{
          if(error.error instanceof ProgressEvent)
          {
            this.loadWorkRequest(id);
          }else
          {
            this.toastr.error(error.error);
          }
        }
      );
  }

  populateControls(workRequest:WorkRequest)
  {
      this.workRequestForm.controls['documentType'].setValue(workRequest.documentType);
      this.workRequestForm.controls['startDate'].setValue(workRequest.startDate);
      this.workRequestForm.controls['endDate'].setValue(workRequest.endDate);
      this.workRequestForm.controls['purpose'].setValue(workRequest.purpose);
      this.workRequestForm.controls['note'].setValue(workRequest.note);
      this.workRequestForm.controls['details'].setValue(workRequest.details);
      this.workRequestForm.controls['companyName'].setValue(workRequest.companyName);
      this.workRequestForm.controls['phone'].setValue(workRequest.phone);
      this.workRequestForm.controls['incident'].setValue(workRequest.incidentID);
      this.workRequestForm.controls['emergency'].setValue(workRequest.isEmergency);
      this.workRequestForm.controls['createdOn'].setValue(workRequest.createdOn);
      this.workRequestForm.controls['status'].setValue(workRequest.documentStatus);
      this.loadUserData(workRequest.userID);
  }

  populateModelFromFields()
  {
      this.workRequest.documentType = this.workRequestForm.controls['documentType'].value;
      this.workRequest.startDate = new Date(this.workRequestForm.controls['startDate'].value);
      this.workRequest.endDate = new Date(this.workRequestForm.controls['endDate'].value);
      this.workRequest.purpose = this.workRequestForm.controls['purpose'].value;
      this.workRequest.note = this.workRequestForm.controls['note'].value;
      this.workRequest.details = this.workRequestForm.controls['details'].value;
      this.workRequest.companyName = this.workRequestForm.controls['companyName'].value;
      this.workRequest.phone = this.workRequestForm.controls['phone'].value;
      this.workRequest.incidentID = +this.workRequestForm.controls['incident'].value;
      this.workRequest.isEmergency = this.workRequestForm.controls['emergency'].value;
      this.workRequest.createdOn = new Date(this.workRequestForm.controls['createdOn'].value);
      this.workRequest.documentStatus = this.workRequestForm.controls['status'].value; 
      this.workRequest.userID = this.user.id;
  }

  loadUserData(id:number)
  {
    this.userService.getById(id).subscribe(
      data =>{
        this.workRequestForm.controls['createdBy'].setValue(`${data.name} ${data.lastname}`);
        this.user  = data;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.loadUserData(id);
        }else
        {
          this.toastr.error(error.error);
        }
      }
    )
  }

  onChooseIncident()
  {
    const dialogRef = this.dialog.open(ChooseIncidentDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.workRequestForm.controls['incident'].setValue(+result);
    });
  }

  onSave()
  {
    if(this.workRequestForm.valid)
    {
        this.populateModelFromFields();
        this.isLoading = true;
        if(this.isNew)
        {
          this.workRequestService.createWorkRequest(this.workRequest).subscribe(
            data =>{
              this.toastr.success("Work request created successfully","", {positionClass: 'toast-bottom-left'});
              this.router.navigate(['work-request/basic-info', data.id])
            },
            error =>{
             this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  this.toastr.error("Server is unreachable");
                }else
                {
                  this.toastr.error(error.error);
                }
              
            }
          )
        }else
        {
          this.workRequestService.updateWorkRequest(this.workRequest).subscribe(
            data =>{
              this.toastr.success("Work request updated successfully","", {positionClass: 'toast-bottom-left'});
              this.workRequest = data;
              this.isLoading = false;
            },
            error =>{
             this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  this.toastr.error("Server is unreachable","", {positionClass: 'toast-bottom-left'});
                }else
                {
                  this.toastr.error(error.error);
                }
              
            }
          )
        }

    }else
    {
      this.validation.validateAllFields(this.workRequestForm);
    }

  }


  logicalDates(c: AbstractControl): {[key: string]: any} |null {
    let startDate = c.get(['startDate']);
    let endDate = c.get(['endDate']);

    if(startDate?.value == "" || new Date(startDate?.value) < new Date())
    {
      c.get(['startDate'])!.setErrors({invalidDates:true});
    } 
    if(endDate?.value == "")
    {
      c.get(['endDate'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }
      

    if (new Date(startDate!.value) >  new Date(endDate!.value)) {
      c.get(['startDate'])!.setErrors({invalidDates:true});
      c.get(['endDate'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }else
    {
      c.get(['startDate'])!.setErrors(null);
      c.get(['endDate'])!.setErrors(null);
      return null;
    }

  }
}
