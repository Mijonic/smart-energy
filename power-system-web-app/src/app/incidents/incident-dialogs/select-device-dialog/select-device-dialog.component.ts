import { AfterViewInit, ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DeviceService } from 'app/services/device.service';
import { IncidentService } from 'app/services/incident.service';
import { Device } from 'app/shared/models/device.model';
import { Location } from 'app/shared/models/location.model';
import { ToastrService } from 'ngx-toastr';




@Component({
  selector: 'app-select-device-dialog',
  templateUrl: './select-device-dialog.component.html',
  styleUrls: ['./select-device-dialog.component.css']
})
export class SelectDeviceDialogComponent implements OnInit {

  
  displayedColumns: string[] = ['id', 'name', 'deviceType', 'coordinates', 'address', 'add'];
  dataSource: MatTableDataSource<Device>;
  isLoading:boolean = true;

  incidentId: number;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  devices:Device[] = [];
  allDevices:Device[] = [];
 

  
 
  



  constructor(public dialogRef: MatDialogRef<SelectDeviceDialogComponent>, private deviceService:DeviceService,private incidentService: IncidentService,  private toastr: ToastrService,
    private route:ActivatedRoute, private router:Router) {


  }


  


  

  ngOnInit(): void {

    

    

    window.dispatchEvent(new Event('resize'));

    this.getUnrelatedDevices();
    this.isLoading = false;
    
    
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

 
  getUnrelatedDevices()
  {
    this.incidentService.getUnrelatedDevices(this.incidentId).subscribe(
      data =>{
        this.allDevices = data;
        this.devices = data;
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoading = false;
       
      },
      error =>{
        this.getUnrelatedDevices();
        this.isLoading = true;
     

    

      }
    )
  }

  getAddressFromLocation(location: Location) {
        
    return  `${location.street} ${location.number}, ${location.city}, ${location.zip}`

  }

  
 

 


  addDeviceToIncident(deviceId: number)
  {

   

    this.incidentService.addDeviceToIncident(this.incidentId, deviceId).subscribe(
      data =>{

        this.getUnrelatedDevices();
        this.isLoading = false;
        this.toastr.success('Device added to incident successfully',"", {positionClass: 'toast-bottom-left'})
        
       
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.addDeviceToIncident(deviceId);

        }else
        {
          this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'})
     
          //this.router.navigate(['incidents']);
          this.dialogRef.close();
          this.isLoading = false;
        }
      }
    )
    

  }

  onCancelClick(): void {
    this.dialogRef.close();
  }






  

}



