import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { IncidentService } from 'app/services/incident.service';
import { SafetyDocumentService } from 'app/services/safety-document.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { UserService } from 'app/services/user.service';
import { ValidationService } from 'app/services/validation.service';
import { SafetyDocument } from 'app/shared/models/safety-document.model';
import { User } from 'app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-safety-document-basic-information',
  templateUrl: './safety-document-basic-information.component.html',
  styleUrls: ['./safety-document-basic-information.component.css']
})
export class SafetyDocumentBasicInformationComponent implements OnInit {


  documentTypes = [
    {display:'Planned work', value:'PLANNED'},
    {display:'Unplanned work', value:'UNPLANNED'},
   ];

  createdOn = new Date((new Date().getTime()));

  user:User = new User();
  safetyDocument:SafetyDocument = new SafetyDocument();

  safetyDocumentForm = new FormGroup({

    documentType: new FormControl('', [Validators.required]),
    workPlanID: new FormControl('', [Validators.required]),
    createdOn: new FormControl({value: new Date(), disabled: true}),
    createdBy: new FormControl({value:'', disabled:true}),
    documentStatus: new FormControl({value:'DRAFT', disabled:true}),
    details: new FormControl('', [Validators.maxLength(100)]),
    phone: new FormControl('', [Validators.maxLength(30)]),
    notes: new FormControl('', [Validators.maxLength(100)]),


    

  });


  isNew = true;
  isLoading:boolean = false;
  safetyDocumentId:number;


  constructor(public dialog:MatDialog, private safetyDocumentsService: SafetyDocumentService, private validation:ValidationService, private route:ActivatedRoute, private userService:UserService,
     private toastr:ToastrService, private router:Router, private incidentService:IncidentService,
    private tabMessaging:TabMessagingService)

    {

    }

  ngOnInit(): void {
    const safetyDocumentId = this.route.snapshot.paramMap.get('id');
      if(safetyDocumentId && safetyDocumentId != "")
      {
        this.tabMessaging.showEdit(+safetyDocumentId);
        this.isNew = false;
        this.safetyDocumentId = +safetyDocumentId;
        this.loadSafetyDocument(this.safetyDocumentId);
      }else
      {
        this.user = JSON.parse(localStorage.getItem("user")!);
        this.safetyDocumentForm.controls['createdBy'].setValue(`${this.user.name} ${this.user.lastname}`);
      }
  }

  loadSafetyDocument(id:number)
  {
    this.isLoading = true;
      this.safetyDocumentsService.getSafetyDocumentById(id).subscribe(
        data =>{
          this.isLoading = false;
          this.safetyDocument = data;
          this.populateControls(this.safetyDocument);
        } ,
        error =>{
          if(error.error instanceof ProgressEvent)
          {
            this.loadSafetyDocument(id);
          }else
          {
            this.toastr.error(error.error);
          }
        }
      );
  }

  populateControls(safetyDocument: SafetyDocument)
  {
    
   

      this.safetyDocumentForm.controls['documentType'].setValue(safetyDocument.documentType);
      this.safetyDocumentForm.controls['workPlanID'].setValue(safetyDocument.workPlanID);
      this.safetyDocumentForm.controls['createdOn'].setValue(safetyDocument.createdOn);

      if(safetyDocument.documentStatus == "APPROVED")
      {
         safetyDocument.documentStatus = "ISSUED";
      }
          
      


      this.safetyDocumentForm.controls['documentStatus'].setValue(safetyDocument.documentStatus);
      this.safetyDocumentForm.controls['details'].setValue(safetyDocument.details);
      this.safetyDocumentForm.controls['phone'].setValue(safetyDocument.phone);
      this.safetyDocumentForm.controls['notes'].setValue(safetyDocument.notes);
  
      this.loadUserData(safetyDocument.userID);
  }

  populateModelFromFields()
  {

   

      this.safetyDocument.documentType = this.safetyDocumentForm.controls['documentType'].value;
      this.safetyDocument.workPlanID = +this.safetyDocumentForm.controls['workPlanID'].value;
      this.safetyDocument.createdOn = this.safetyDocumentForm.controls['createdOn'].value;
      //this.safetyDocument.documentStatus = this.safetyDocumentForm.controls['documentStatus'].value;
      if(this.safetyDocumentForm.controls['documentStatus'].value == "ISSUED")
      {
         this.safetyDocument.documentStatus = "APPROVED";
      }else
      {
        this.safetyDocument.documentStatus = this.safetyDocumentForm.controls['documentStatus'].value;
      }
      this.safetyDocument.details = this.safetyDocumentForm.controls['details'].value;
      this.safetyDocument.phone = this.safetyDocumentForm.controls['phone'].value;
      this.safetyDocument.notes = this.safetyDocumentForm.controls['notes'].value;    
      this.safetyDocument.userID = this.user.id;



    
  }

  loadUserData(id:number)
  {
    this.userService.getById(id).subscribe(
      data =>{
        this.safetyDocumentForm.controls['createdBy'].setValue(`${data.name} ${data.lastname}`);
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

  onSave()
  {
    console.log("pokusaj cuvanja");
    if(this.safetyDocumentForm.valid)
    {

      
        this.populateModelFromFields();
        this.isLoading = true;
        console.log(this.safetyDocument);

        if(this.isNew)
        {
          this.safetyDocumentsService.createNewSafetyDocument(this.safetyDocument).subscribe(
            data =>{
              this.toastr.success("Safety document created successfully","", {positionClass: 'toast-bottom-left'});
              this.router.navigate(['safety-document/basic-info', data.id])
            },
            error =>{
             this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  this.toastr.error("Server is unreachable");
                }else
                {
                  console.log(error.error);
                 
                  this.toastr.error(error.error);
                }
              
            }
          )
        }else
        {
          this.safetyDocumentsService.updateSafetyDocument(this.safetyDocument).subscribe(
            data =>{
              this.toastr.success("Safety document updated successfully","", {positionClass: 'toast-bottom-left'});
              this.safetyDocument = data;
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
      this.validation.validateAllFields(this.safetyDocumentForm);
    }

  }




}


