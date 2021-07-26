import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectConsumerDialogComponent } from 'app/incidents/incident-dialogs/select-consumer-dialog/select-consumer-dialog.component';
import { DeviceService } from 'app/services/device.service';
import { IncidentService } from 'app/services/incident.service';
import { LocationService } from 'app/services/location.service';
import { ValidationService } from 'app/services/validation.service';
import { Call } from 'app/shared/models/call.model';
import { Consumer } from 'app/shared/models/consumer.model';
import { Location } from 'app/shared/models/location.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-new-call',
  templateUrl: './new-call.component.html',
  styleUrls: ['./new-call.component.css']
})
export class NewCallComponent implements OnInit {
  @Input()
  reasons:string[];

  @Output() newCallFinished: EventEmitter<any> = new EventEmitter();

  incidentId: number;
  isCustomerSelected = false;

  loadLocations = true;

  consumer: Consumer = new Consumer();

  submitted = false;
  allLocations: Location[] = [];

  isNew:boolean = true;

  call: Call = new Call();
  newCall:Call = new Call();

  newCallForm = new FormGroup({
      callReason : new FormControl('', Validators.required),
      hazard : new FormControl('', [Validators.required, Validators.maxLength(100)]),
      comment : new FormControl('', [Validators.maxLength(100)]),
      callLocationControl : new FormControl('', Validators.required)
   });


   /*
constructor(private locationService:LocationService, private validationService:ValidationService, private deviceService:DeviceService,  private toastr: ToastrService,
    private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {

    this.getAllLocations();
    
    const deviceId = this.route.snapshot.paramMap.get('id');
    if(deviceId != null && deviceId != "")
    {
      this.isNew = false;
      this.loadDevice(+deviceId);
    }
  
   

  }

   */

  constructor(public dialog:MatDialog, private locationService:LocationService, private validationService:ValidationService, private deviceService:DeviceService,  private toastr: ToastrService,
    private router:Router, private route:ActivatedRoute, private incidentService: IncidentService) { 
      this.consumer.location = new Location();
    }


    ngOnInit(): void {

      this.getAllLocations();
      
      const incidentId = this.route.snapshot.paramMap.get('id');
      if(incidentId != null && incidentId != "")
      {
        this.incidentId = +incidentId;
        
      }
    
     
  
    }

    
  getAllLocations()
  {
    this.locationService.getAllLocations().subscribe(
      data =>{
        this.allLocations = data;
        this.loadLocations = false;
   
      },
      error =>{
       

        if(error.error instanceof ProgressEvent)
        {
          this.getAllLocations();

        }else
        {
          this.toastr.error('Could not load locations.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.loadLocations = false;
        }

      }
    )
  }
  

  onSave()
  {

    this.submitted = true;

    if(this.newCallForm.valid)
    {
     
      
         this.call.callReason = this.newCallForm.value.callReason;
         this.call.comment = this.newCallForm.value.comment;
         this.call.hazard = this.newCallForm.value.hazard;
         

         if(this.isCustomerSelected)
         {
          this.call.locationId = +this.consumer.locationID;
          this.call.consumerId = +this.consumer.id;
         }else
         {
          this.call.locationId = +this.newCallForm.value.callLocationControl;
         }

         
       

          
          this.incidentService.addIncidentCall(this.incidentId, this.call).subscribe(
            data => {
            
              this.toastr.success("Incident call created successfully","", {positionClass: 'toast-bottom-left'});
              //this.router.navigate(['incidents']);

              this.newCallFinished.emit();
              },
              error=>{
                
                this.toastr.error(error.error, "", {positionClass: 'toast-bottom-left'});
              
              }
          );
      


    }else
    {
      this.validationService.validateAllFields(this.newCallForm);
    
    }

   
  }





  getAddressFromLocation(location: Location) {
        
    return  `${location.street} ${location.number}, ${location.city}, ${location.zip}`

  }


  onChooseCustomer()
  {
    const dialogRef = this.dialog.open(SelectConsumerDialogComponent, {width: "70%"});
    

    dialogRef.afterClosed().subscribe(result => {
      if(result)
      {
        
      
        let tempConsumer = result as Consumer;
        this.consumer.id = tempConsumer.id;
        this.consumer.name = tempConsumer.name;
        this.consumer.lastname = tempConsumer.lastname;
        this.consumer.accountID = tempConsumer.accountID;
        this.consumer.accountType = tempConsumer.accountType;
        this.consumer.location = tempConsumer.location;
        this.consumer.locationID = tempConsumer.locationID;

        this.isCustomerSelected = true;
        this.newCallForm.controls['callLocationControl'].disable();


       

        
        
      }
    });

   
    console.log("nakon izlaska")
    console.log(this.consumer)
    // dialogRef.afterClosed().subscribe(result => {

      
    //   // if(result)
    //   //   this.workRequestForm.controls['incident'].setValue(+result);
    // });

   
  }

}
