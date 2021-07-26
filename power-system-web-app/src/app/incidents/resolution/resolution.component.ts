import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ResolutionService } from 'app/services/resolution.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ValidationService } from 'app/services/validation.service';
import { Resolution } from 'app/shared/models/resolution.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-resolution',
  templateUrl: './resolution.component.html',
  styleUrls: ['./resolution.component.css']
})
export class ResolutionComponent implements OnInit {

  failureSubcauses:string[] = ['BURNED_OUT', 'SHORT_CIRCUIT', 'MECHANICAL_FAILURE'];
  weatherSubcauses:string[] = ['STORM', 'RAIN', 'WIND', 'SNOW'];
  humanErrorSubcauses:string[] = ['BAD_INSTALL', 'NO_SUPERVISION', 'UNDER_VOLTAGE'];
  subcauses:string[] = [];

  incidentId: number = 0;
  resolutionId: number = 0;
  loadedResolution: boolean = false;

  submitted = false;
  isNew:boolean = true;

  defaultSubcause: string = "";

  resolution: Resolution = new Resolution();
  newResolution: Resolution = new Resolution();

  newResolutionForm = new FormGroup({
      causeControl : new FormControl('', Validators.required),
      subcauseControl : new FormControl('', Validators.required),
      constructionControl : new FormControl('', Validators.required),
      materialControl : new FormControl('', Validators.required)
   });

  




  constructor(private tabMessaging:TabMessagingService, private route:ActivatedRoute,private validationService:ValidationService, private resolutionService:ResolutionService,  private toastr: ToastrService,
    private router:Router) { }




  ngOnInit(): void {

    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);    
      this.incidentId = +incidentId;   
      this.loadIncidentResolution(+incidentId);


    }

    window.dispatchEvent(new Event('resize'));
    
    
  }

  causeSelectionChanged(event:any)
  {
    if(event.value === 'FAILURE')
    {
      this.subcauses = this.failureSubcauses;

    }else if(event.value === 'WEATHER')
    {
      this.subcauses = this.weatherSubcauses;

    }else if(event.value === 'HUMAN_ERROR')
    {
      this.subcauses = this.humanErrorSubcauses;
    }

    this.defaultSubcause = this.subcauses[0];
    this.newResolutionForm.get('subcauseControl')!.setValue(this.defaultSubcause);

 
   
  }

  get shouldShowSubcauses():boolean{
   
    return this.subcauses.length > 0;

    
  }

  loadIncidentResolution(incidentId:number){
    console.log(incidentId)
    this.resolutionService.getResolutionIncident(incidentId).subscribe(
      data =>{
        this.resolution = data;
        this.loadedResolution = true;

        this.resolutionId = data.id;
        this.isNew = false;

        if(this.resolution.cause === 'FAILURE')
          {
            this.subcauses = this.failureSubcauses;

          }else if(this.resolution.cause === 'WEATHER')
          {
            this.subcauses = this.weatherSubcauses;

          }else if(this.resolution.cause === 'HUMAN_ERROR')
          {
            this.subcauses = this.humanErrorSubcauses;
          }else
          {
            this.subcauses = [];
          }

          this.defaultSubcause = this.subcauses[0];
          
        
        console.log(this.resolution.subcause.toString())
        this.newResolutionForm.setValue({
          causeControl: this.resolution.cause.toString(),
          subcauseControl: this.resolution.subcause.toString(),
          constructionControl: this.resolution.construction.toString(),
          materialControl: this.resolution.material.toString()
       });
        
      
       
      },
      error =>{
       
        this.resolution.id = 0;
      }
    );
  }


  saveChanges()
  {
    this.submitted = true;

    if(this.newResolutionForm.valid)
    {
     
      if(this.isNew)
      {
        
          this.newResolution.cause = this.newResolutionForm.value.causeControl;
          this.newResolution.subcause = this.newResolutionForm.value.subcauseControl;
          this.newResolution.construction = this.newResolutionForm.value.constructionControl;
          this.newResolution.material = this.newResolutionForm.value.materialControl;
          this.newResolution.incidentId = this.incidentId;

         
          this.resolutionService.createNewResolution(this.newResolution).subscribe(
            data => {
            
              this.toastr.success("Resolution created successfully","", {positionClass: 'toast-bottom-left'});
              this.router.navigate(['incident/resolution', data.id])
              },
              error=>{
                this.toastr.error(error.error);
              
              }
          );
      }else
      {
          this.newResolution.id = this.resolutionId;
          this.newResolution.cause = this.newResolutionForm.value.causeControl;
          this.newResolution.subcause = this.newResolutionForm.value.subcauseControl;
          this.newResolution.construction = this.newResolutionForm.value.constructionControl;
          this.newResolution.material = this.newResolutionForm.value.materialControl;
          this.newResolution.incidentId = this.incidentId;

          this.resolutionService.updateResolution(this.newResolution).subscribe(
              data => {
                this.resolution = data;
                this.toastr.success("Resolution updated successfully","", {positionClass: 'toast-bottom-left'});
                this.router.navigate(['incident/resolution', data.id])
              },
              error=>{
                this.router.navigate(['incident/resolution', this.incidentId])
                this.toastr.error(error.error);
              }
           );
        

      }


    }else
    {
      this.validationService.validateAllFields(this.newResolutionForm);
    }
  }







}
