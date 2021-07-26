import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DeviceService } from 'app/services/device.service';
import { IncidentService } from 'app/services/incident.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ValidationService } from 'app/services/validation.service';
import { Device } from 'app/shared/models/device.model';
import { Location } from 'app/shared/models/location.model';
import { ToastrService } from 'ngx-toastr';
import { SelectDeviceDialogComponent } from '../incident-dialogs/select-device-dialog/select-device-dialog.component';




@Component({
  selector: 'app-incident-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})
export class IncidentDevicesComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['id', 'name', 'deviceType', 'coordinates', 'address', 'map', 'remove'];
  dataSource: MatTableDataSource<Device>;
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  incidentDevices:Device[] = [];
  allIncidentDevices:Device[] = [];

  incidentId: number;

  
  filterDevices = new FormGroup({
     
    reasonsControl: new FormControl('')  

  });




  constructor(public dialog:MatDialog, private incidentService: IncidentService, private tabMessaging:TabMessagingService, private route:ActivatedRoute,
    private validation:ValidationService, private toastr:ToastrService, private router:Router,) {

  }
  




  ngOnInit(): void {

    window.dispatchEvent(new Event('resize'));
    const incidentId = this.route.snapshot.paramMap.get('id');
    console.log( this.route.snapshot);
    
    
    if(incidentId && incidentId != "")
    {
       this.tabMessaging.showEdit(+incidentId);
     
       this.incidentId = +incidentId;
       this.getIncidentDevices(this.incidentId);
    }

    
   

    
    
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }



  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  getIncidentDevices(incidentId: number)
  {
    this.incidentService.getIncidentDevices(incidentId).subscribe(
      data =>{
        this.allIncidentDevices = data;
        this.incidentDevices = data;
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

        this.isLoading = false;
       
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.getIncidentDevices(incidentId);
        }else
        {
          this.toastr.error('Could not load incident devices.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
      }
    )
  }

  removeDeviceFromIncident(deviceId:number)
  {
    this.incidentService.removeDeviceFromIncident(this.incidentId, deviceId).subscribe(
      data =>{

        this.getIncidentDevices(this.incidentId);
        this.toastr.success("Device removed from incident successfully","", {positionClass: 'toast-bottom-left'});
        this.isLoading = false;
       
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.removeDeviceFromIncident(deviceId);
        }else
        {
          this.toastr.error('Could not remove device from incident.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
      }
    )
  }


  onAddDevice()
  {
    const dialogRef = this.dialog.open(SelectDeviceDialogComponent, {width: "70%"});
    dialogRef.componentInstance.incidentId = this.incidentId;

    dialogRef.afterClosed().subscribe((result: any) => {
      //console.log(`The dialog was closed and choosen id is ${result}`);
      this.getIncidentDevices(this.incidentId);
    });
  }


  getAddressFromLocation(location: Location) {
        
    return  `${location.street} ${location.number}, ${location.city}, ${location.zip}`

  }


}

