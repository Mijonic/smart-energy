import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
//import { MatTableDataSource } from '@angular/material/table';
import { DeviceService } from 'app/services/device.service';
import { Device } from 'app/shared/models/device.model';
import { ToastrService } from 'ngx-toastr';
import { Location } from 'app/shared/models/location.model'; 
import { LocationService } from 'app/services/location.service';
import { merge, Observable, of } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';



@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})


export class DevicesComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['action', 'id', 'name', 'type', 'coordinates', 'address', 'map'];
  dataSource:Observable<Device[]>;    

  isLoading:boolean = true;

  

  devices:Device[] = [];
  allDevices:Device[] = [];
  //allLocations:Location[] = [];

  searchForm = new FormGroup(
    {
      searchControl:new FormControl(''),
      typeControl:new FormControl('all'),
      fieldControl:new FormControl('')
    }
  );

  
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;



  constructor(private deviceService:DeviceService,  private toastr: ToastrService) {


  }

  ngOnInit(): void {
   
    //this.getDevices();
    
    
  }


  ngAfterViewInit(): void {
    if(this.isLoading)
      this.getDevicesPaged();
  }

  getDevicesPaged() {

   
    this.dataSource = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.deviceService.getDevicesPaged(
             this.paginator.pageIndex, this.paginator.pageSize, this.sort.active, this.sort.direction);
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoading = false;
          this.paginator.length = data.totalCount;

          return data.devices;
        }),
        catchError(() => {
          this.isLoading = false;
          this.toastr.error("Device service is unavailable right now. Please, try later.","", {positionClass: 'toast-bottom-left'});
          return of([]);
        })
      );
  }
 

  search() {

    let type = this.searchForm.controls['typeControl'].value;
    let field = this.searchForm.controls['fieldControl'].value;
    let search = this.searchForm.controls['searchControl'].value;

    this.dataSource = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.deviceService.getSearchDevicesPaged(
             this.paginator.pageIndex, this.paginator.pageSize, this.sort.active, this.sort.direction, type, field, search );
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoading = false;
          this.paginator.length = data.totalCount;

          return data.devices;
        }),
        catchError(() => {
          this.isLoading = false;
          return of([]);
        })
      );

      this.resetPaging();
  }
 

 
  // getDevices()
  // {
  //   this.deviceService.getAllDevices().subscribe(
  //     data =>{
  //       this.allDevices = data;
  //       this.devices = data;
  //       this.dataSource = new MatTableDataSource(data);
  //       this.isLoading = false;
       
  //     },
  //     error =>{
  //       this.getDevices();
  //     }
  //   )
  // }


  resetPaging(): void {
    this.paginator.pageIndex = 0;
   
  }

  resetSearch()
  {
    this.searchForm.setValue({
      searchControl: "",
      typeControl: "",
      fieldControl: ""
   });

 
  }


  

  getAddressFromLocation(location: Location) {
        
    return  `${location.street} ${location.number}, ${location.city}, ${location.zip}`

  }

  
  delete(deviceId: number)
  {
   
    this.deviceService.deleteDevice(deviceId).subscribe(x =>{
        this.getDevicesPaged();
        this.toastr.success("Device successfully deleted","", {positionClass: 'toast-bottom-left'});
    });
  }



 

}


