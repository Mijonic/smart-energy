import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DeviceService } from 'app/services/device.service';
import { LocationService } from 'app/services/location.service';
import { ValidationService } from 'app/services/validation.service';
import { Device } from 'app/shared/models/device.model';
import { Location } from 'app/shared/models/location.model';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { Options } from 'tsparticles/dist/Options/Classes/Options';

@Component({
  selector: 'app-new-device',
  templateUrl: './new-device.component.html',
  styleUrls: ['./new-device.component.css']
})
export class NewDeviceComponent implements OnInit {

  loadLocations = true;
  loadedDevice = true;

  submitted = false;
  allLocations: Location[] = [];

  isNew:boolean = true;

  device: Device = new Device();
  timestamp: Date;

  newDevice:Device = new Device();
  newDeviceForm = new FormGroup({
      deviceTypeControl : new FormControl('', Validators.required),
      deviceLocationControl : new FormControl('', Validators.required)
   });


  //autocomplete filter
  //options: string[] = [];
  filteredOptions: Observable<Location[]>;




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


  getAllLocations()
  {
    this.locationService.getAllLocations().subscribe(
      data =>{
        this.allLocations = data;
        this.loadLocations = false;
   
      },
      error =>{
        this.getAllLocations();
      }
    )
  }

  loadDevice(id:number){
    this.deviceService.getDeviceById(id).subscribe(
      data =>{
        this.device = data;
        this.loadedDevice = false;

        this.timestamp = this.device.timestamp;

        console.log(this.device)

        this.newDeviceForm.setValue({
          deviceTypeControl: this.device.deviceType.toString(),
          deviceLocationControl: this.device.location.id.toString()
       });
      

       
      },
      error =>{
        this.toastr.error('Could not load device.',"", {positionClass: 'toast-bottom-left'})
       
        this.router.navigate(['devices']);
        this.loadedDevice = false;
      }
    );
  }

  

  saveChanges()
  {
    this.submitted = true;

    if(this.newDeviceForm.valid)
    {
     
      if(this.isNew)
      {
         
          this.device.deviceType = this.newDeviceForm.value.deviceTypeControl;
          this.device.locationId = +this.newDeviceForm.value.deviceLocationControl;
          this.device.timestamp = this.timestamp;


          
          this.deviceService.createNewDevice(this.device).subscribe(
            data => {
            
              this.toastr.success("Device created successfully","", {positionClass: 'toast-bottom-left'});
              this.router.navigate(['devices']);
              },
              error=>{
                this.router.navigate(['devices']);
                this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'});
              
              }
          );
      }else
      {
          this.device.deviceType = this.newDeviceForm.value.deviceTypeControl;
          this.device.locationId = +this.newDeviceForm.value.deviceLocationControl;
          this.device.timestamp = this.timestamp;

          this.deviceService.updateDevice(this.device).subscribe(
              data => {
                this.device = data;
                this.toastr.success("Device updated successfully","", {positionClass: 'toast-bottom-left'});
                this.router.navigate(['devices']);
              },
              error=>{
                this.router.navigate(['devices']);
                this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'});
              }
           );
        

      }


    }else
    {
      this.validationService.validateAllFields(this.newDeviceForm);
    }
  }

  getAddressFromLocation(location: Location) {
        
    return  `${location.street} ${location.number}, ${location.city}, ${location.zip}`

  }

 
 
}
