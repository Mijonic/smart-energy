import { Time } from '@angular/common';
import { AttrAst } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { ActivatedRoute, Router } from '@angular/router';
import { IncidentService } from 'app/services/incident.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ValidationService } from 'app/services/validation.service';
import { Incident } from 'app/shared/models/incident.model';
import { User } from 'app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';
import { isObservable } from 'rxjs';

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrls: ['./basic-information.component.css']
})
export class BasicInformationComponent implements OnInit {

  documentTypes = [
    {display:'Planned work', value:'PLANNED'},
    {display:'Unplanned work', value:'UNPLANNED'},
   ];

   incidentStatuses = [
    {display:'Unresolved', value:'UNRESOLVED'},
    {display:'Resolved', value:'RESOLVED'},
    
   ];
   
   showSpinners = true;
   showSeconds = true;
   touchUi = true;
   enableMeridian = true;

   


   

  
   incidentForm = new FormGroup({
     
    confirmed: new FormControl(false),
    eta: new FormControl('', Validators.required),
    ata: new FormControl('', Validators.required),
    etr: new FormControl('', Validators.required),
    workBeginDate: new FormControl('', Validators.required),
    incidentDateTime: new FormControl('', Validators.required),
    voltageLevel: new FormControl('', [Validators.required, Validators.min(0.1)]),
    description: new FormControl('', [Validators.maxLength(100)]),
    workType: new FormControl('', Validators.required),
    incidentStatus: new FormControl('', Validators.required)
   

  }, {validators:[this.logicalDatesIncidentATA, this.logicalDatesIncidentETA, this.logicalDatesIncidentWorkBegin]} );

  
   

  isNew = true;
  isLoading:boolean = false;
  incidentId:number;
  timestamp: Date;

  incident: Incident = new Incident();
  
  affectedConsumers: number = 0;
  numberOfCalls: number = 0;
  priority: number = 0;
    
  user:User = new User();



  constructor(private tabMessaging:TabMessagingService, private route:ActivatedRoute,
    private validation:ValidationService,   private toastr:ToastrService, private incidentService:IncidentService, private router:Router) { }

  ngOnInit(): void {
    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);
      this.isNew = false;
      this.incidentId = +incidentId;
      this.loadIncident(this.incidentId);
      this.getNumberOfAffectedConsumers(this.incidentId);
      this.getNumberOfIncidentCalls(this.incidentId);
    }else
    {
      this.user = JSON.parse(localStorage.getItem("user")!);
     
    }
  }


  loadIncident(id:number)
  {
    this.isLoading = true;
      this.incidentService.getIncidentById(id).subscribe(
        data =>{
          this.isLoading = false;
          this.incident = data;
          this.priority = this.incident.priority;
          this.timestamp = this.incident.timestamp;
          this.populateControls(this.incident);
        } ,
        error =>{
          if(error.error instanceof ProgressEvent)
          {
            this.loadIncident(id);
          }else
          {
            this.toastr.error('Could not load incident.',"", {positionClass: 'toast-bottom-left'})
       
            this.router.navigate(['incidents']);
            this.isLoading = false;
          }
        }
      );

     
  }

  getNumberOfAffectedConsumers(id: number)
  {
    
      this.incidentService.getNumberOfAffectedConsumers(id).subscribe(
        data =>{
         
          this.affectedConsumers = +data;   
        } ,
        error =>{
          if(error.error instanceof ProgressEvent)
          {
            this.getNumberOfAffectedConsumers(id);
          }else
          {
            this.toastr.error('Could not load number of affected consumers.',"", {positionClass: 'toast-bottom-left'})
       
            this.router.navigate(['incidents']);
            this.isLoading = false;
          }
        }
      );

  }


  getNumberOfIncidentCalls(id: number){

    this.incidentService.getNumberOfIncidentCalls(id).subscribe(
      data =>{
       
        this.numberOfCalls = +data;   
      } ,
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.getNumberOfIncidentCalls(id);
        }else
        {
          this.toastr.error('Could not load number of incident calls.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
      }
    );
  }



  populateControls(incident:Incident)
  {

      
    
     
      this.incidentForm.controls['confirmed'].setValue(incident.confirmed);
      this.incidentForm.controls['eta'].setValue(incident.eta);
      this.incidentForm.controls['ata'].setValue(incident.ata);
      this.incidentForm.controls['etr'].setValue(incident.etr);
      this.incidentForm.controls['workBeginDate'].setValue(incident.workBeginDate);
      this.incidentForm.controls['incidentDateTime'].setValue(incident.incidentDateTime);
      this.incidentForm.controls['voltageLevel'].setValue(incident.voltageLevel);
      this.incidentForm.controls['description'].setValue(incident.description);
      this.incidentForm.controls['workType'].setValue(incident.workType);
      this.incidentForm.controls['incidentStatus'].setValue(incident.incidentStatus.toString());

   
     

     
    
      //this.loadUserData(workRequest.userID);
  }

  populateModelFromFields()
  {
    
     
      this.incident.confirmed = 
      this.incident.confirmed =  this.incidentForm.controls['confirmed'].value;
      this.incident.eta = new Date(this.incidentForm.controls['eta'].value);
      this.incident.ata = new Date(this.incidentForm.controls['ata'].value);
      //this.incident.etr =  new Date(this.incidentForm.controls['etr'].value);
      this.incident.etr = new Date();
      this.splitTime(this.incident.etr, this.incidentForm.controls['etr'].value );
      this.incident.workBeginDate =  new Date(this.incidentForm.controls['workBeginDate'].value);
      this.incident.incidentDateTime =  new Date (this.incidentForm.controls['incidentDateTime'].value);
      this.incident.voltageLevel = +this.incidentForm.controls['voltageLevel'].value;
      this.incident.description = this.incidentForm.controls['description'].value;
      this.incident.workType = this.incidentForm.controls['workType'].value;
      this.incident.incidentStatus = this.incidentForm.controls['incidentStatus'].value;

     // console.log(this.incident.etr);
    
      //this.incident.userId = 4;
      if(this.isNew)
      {
        this.incident.userId = this.user.id;
       
      }
  }



  onSave()
  {
    if(this.incidentForm.valid)
    {
        this.populateModelFromFields();
        this.incident.timestamp = this.timestamp;
        this.isLoading = true;
       
        if(this.isNew)
        {
         
          this.incidentService.createNewIncident(this.incident).subscribe(
            data =>{
              console.log(data)
              this.toastr.success('Incident created successfully',"", {positionClass: 'toast-bottom-left'})
              this.router.navigate(['incident/basic-info', data.id])
            },
            error =>{
            this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  
                  this.toastr.error('Server is unreachable',"", {positionClass: 'toast-bottom-left'})
                }else
                {
                  
                  console.log(error.error);
                  
                  
                  this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'})
                }
              
            }
          )
        }else
        {
          
          this.incidentService.updateIncident(this.incident).subscribe(
            data =>{
              this.toastr.success('Incident updated successfully',"", {positionClass: 'toast-bottom-left'})
              this.incident = data;
              this.isLoading = false;
              
             
              
            },
            error =>{
            this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  
                  this.toastr.success('Server is unreachable',"", {positionClass: 'toast-bottom-left'})
                }else
                {
                  
                  this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'})
                }
                
              
            }
          )

        
        }

    }else
    {
      this.validation.validateAllFields(this.incidentForm);
    }

  }


  
  logicalDatesIncidentETA(c: AbstractControl): {[key: string]: any} |null {
    let incidentDateTime = c.get(['incidentDateTime']);
    let eta = c.get(['eta']);
   

    if(incidentDateTime?.value == "")
    {
      c.get(['incidentDateTime'])!.setErrors({invalidDates:true});
    } 


    if(eta?.value == "")
    {
      c.get(['eta'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }

   
    if (new Date(incidentDateTime!.value) >  new Date(eta!.value)) {
      c.get(['incidentDateTime'])!.setErrors({invalidDates:true});
      c.get(['eta'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }else
    {
      c.get(['incidentDateTime'])!.setErrors(null);
      c.get(['eta'])!.setErrors(null);
      return null;
    }



  }


  logicalDatesIncidentATA(c: AbstractControl): {[key: string]: any} |null {
    let incidentDateTime = c.get(['incidentDateTime']);
    let ata = c.get(['ata']);
   

    if(incidentDateTime?.value == "")
    {
      c.get(['incidentDateTime'])!.setErrors({invalidDates:true});
    } 


    if(ata?.value == "")
    {
      c.get(['ata'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }

   
    if (new Date(incidentDateTime!.value) >  new Date(ata!.value)) {
      c.get(['incidentDateTime'])!.setErrors({invalidDates:true});
      c.get(['ata'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }else
    {
      c.get(['incidentDateTime'])!.setErrors(null);
      c.get(['ata'])!.setErrors(null);
      return null;
    }



  }
  
  logicalDatesIncidentWorkBegin(c: AbstractControl): {[key: string]: any} |null {
    let incidentDateTime = c.get(['incidentDateTime']);
    let workBeginDate = c.get(['workBeginDate']);
   

    if(incidentDateTime?.value == "")
    {
      c.get(['incidentDateTime'])!.setErrors({invalidDates:true});
    } 


    if(workBeginDate?.value == "")
    {
      c.get(['workBeginDate'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }

   
    if (new Date(workBeginDate!.value) <  new Date(incidentDateTime!.value)) {
      c.get(['incidentDateTime'])!.setErrors({invalidDates:true});
      c.get(['workBeginDate'])!.setErrors({invalidDates:true});
      return { invalidDates: true };
    }else
    {
      c.get(['incidentDateTime'])!.setErrors(null);
      c.get(['workBeginDate'])!.setErrors(null);
      return null;
    }



  }

   splitTime(etrDate: Date, time:string)
   {
     
      let splitted = time.split(":"); 
   
      etrDate.setHours( +splitted[0], +splitted[1]);
      console.log(splitted);
     
      

   }
 



}
