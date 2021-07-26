import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { DisplayService } from 'app/services/display.service';
import { IncidentService } from 'app/services/incident.service';
import { SafetyDocumentService } from 'app/services/safety-document.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ValidationService } from 'app/services/validation.service';
import { Checklist } from 'app/shared/models/checklist.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-safety-document-checklist',
  templateUrl: './safety-document-checklist.component.html',
  styleUrls: ['./safety-document-checklist.component.css']
})
export class SafetyDocumentChecklistComponent implements OnInit {

  safetyDocumentId: number;

  checklist: Checklist = new Checklist();
  isNew:boolean = true;

  load: boolean = false;



  newChecklistForm = new FormGroup({
      tagsRemovedControl : new FormControl('', Validators.required),
      readyControl : new FormControl('', Validators.required),
      groundingRemovedControl : new FormControl('', Validators.required),
      operationCompletedControl : new FormControl('', Validators.required),
   });
  


  constructor(public dialog:MatDialog, private validation:ValidationService, private router:Router, private safetyDocumentService: SafetyDocumentService,  private route:ActivatedRoute, private toastr:ToastrService,
    private tabMessaging:TabMessagingService, public display:DisplayService) {
  }

  ngOnInit(): void {
    const sf = this.route.snapshot.paramMap.get('id');

    if(sf != null && sf != "")
    {
      this.tabMessaging.showEdit(+sf!);
      this.isNew = false;
      this.safetyDocumentId = +sf;

      this.loadSafetyDocument(this.safetyDocumentId);
    }

  
  
  }

  loadSafetyDocument(id:number){
    this.safetyDocumentService.getSafetyDocumentById(id).subscribe(
      data =>{
        this.checklist.safetyDocumentId = data.id;
        this.checklist.tagsRemoved = data.tagsRemoved;
        this.checklist.ready = data.ready;
        this.checklist.groundingRemoved = data.groundingRemoved;
        this.checklist.operationCompleted = data.operationCompleted;



        this.load = false;

       

        this.newChecklistForm.setValue({
          tagsRemovedControl: this.checklist.tagsRemoved.toString(),
          readyControl: this.checklist.ready.toString(),
          groundingRemovedControl: this.checklist.groundingRemoved.toString(),
          operationCompletedControl: this.checklist.operationCompleted.toString()
       });

      
      

       
      },
      error =>{
        this.toastr.error('Could not load checklist.',"", {positionClass: 'toast-bottom-left'})
       
        this.router.navigate(['safety-documents']);
        this.load = false;
      }
    );
  }

  

  onSave()
  {
    

    if(this.newChecklistForm.valid)
    {
     
      if(this.isNew)
      {
        
         

          this.checklist.safetyDocumentId =  this.safetyDocumentId;
          this.checklist.tagsRemoved =  JSON.parse(this.newChecklistForm.value.tagsRemovedControl); 
          this.checklist.ready =  JSON.parse(this.newChecklistForm.value.readyControl);
          this.checklist.groundingRemoved = JSON.parse( this.newChecklistForm.value.groundingRemovedControl);
          this.checklist.operationCompleted =  JSON.parse(this.newChecklistForm.value.operationCompletedControl);

          console.log(this.checklist);
          
          this.safetyDocumentService.updateSafetyDocumentChecklist(this.checklist).subscribe(
            data => {
            
              this.toastr.success("Checklist created successfully","", {positionClass: 'toast-bottom-left'});
            
              },
              error=>{
                this.toastr.error(error.error);
                console.log(error.error);
               
              
              }
          );
      }else
      {

       

        this.checklist.safetyDocumentId =  this.safetyDocumentId;
        this.checklist.tagsRemoved =  JSON.parse(this.newChecklistForm.value.tagsRemovedControl); 
        this.checklist.ready =  JSON.parse(this.newChecklistForm.value.readyControl);
        this.checklist.groundingRemoved = JSON.parse( this.newChecklistForm.value.groundingRemovedControl);
        this.checklist.operationCompleted =  JSON.parse(this.newChecklistForm.value.operationCompletedControl);

          console.log(this.checklist);

          this.safetyDocumentService.updateSafetyDocumentChecklist(this.checklist).subscribe(
              data => {

                

                this.checklist.safetyDocumentId = +data.safetyDocumentId;
                this.checklist.tagsRemoved = data.tagsRemoved;
                this.checklist.ready = data.ready;
                this.checklist.groundingRemoved = data.groundingRemoved;
                this.checklist.operationCompleted = data.operationCompleted;
                
        
               
                this.toastr.success("Checklist updated successfully","", {positionClass: 'toast-bottom-left'});
               
              },
              error=>{
                this.router.navigate(['safety-documents']);
                this.toastr.error(error.error);
                console.log(error.error);
              }
           );
        

      }


    }else
    {
      this.validation.validateAllFields(this.newChecklistForm);
    }
  }


 

  

}
