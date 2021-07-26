import { DisplayService } from './../../../services/display.service';
import { ToastrService } from 'ngx-toastr';
import { DeviceService } from './../../../services/device.service';
import { Device } from './../../../shared/models/device.model';

import { AfterViewInit, ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';


@Component({
  selector: 'app-choose-equipment-dialog',
  templateUrl: './choose-equipment-dialog.component.html',
  styleUrls: ['./choose-equipment-dialog.component.css']
})
export class ChooseEquipmentDialogComponent implements OnInit, AfterViewInit{
 
  displayedColumns: string[] = ['id', 'name', 'type', 'coordinates', 'address', 'action'];
  dataSource: MatTableDataSource<Device>;
  toppings = new FormControl();
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato']; 
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(public dialogRef: MatDialogRef<ChooseEquipmentDialogComponent>, private deviceService:DeviceService, private toastr:ToastrService,
    public display:DisplayService) {

   }

   loadDevices(){
    this.isLoading = true;
    this.deviceService.getAllDevices().subscribe(
      data =>{
        this.dataSource = new MatTableDataSource(data);
        this.isLoading = false;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.loadDevices();
        }else
        {
          this.isLoading = false;
          this.toastr.error(error.error);
        }
      }
    )
   }

  
   ngOnInit(): void {
    this.loadDevices();
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

  onCancelClick(): void {
    this.dialogRef.close();
  }


  

}
