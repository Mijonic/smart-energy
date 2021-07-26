import { ChooseEquipmentDialogComponent } from './../../dialogs/choose-equipment-dialog/choose-equipment-dialog.component';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ValidationService } from 'app/services/validation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { Device } from 'app/shared/models/device.model';
import { SafetyDocumentService } from 'app/services/safety-document.service';
import { DisplayService } from 'app/services/display.service';



@Component({
  selector: 'app-safety-document-equipment',
  templateUrl: './safety-document-equipment.component.html',
  styleUrls: ['./safety-document-equipment.component.css']
})
export class SafetyDocumentEquipmentComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'deviceType', 'coordinates', 'address', 'actions'];
  dataSource: MatTableDataSource<Device>;
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public dialog:MatDialog, private safetyDocumentService:SafetyDocumentService, private route:ActivatedRoute, private toastr:ToastrService,
    private tabMessaging:TabMessagingService, public display:DisplayService) {
  }

  ngOnInit(): void {
    const sf = this.route.snapshot.paramMap.get('id');
    console.log(sf);
    this.loadDevices(+sf!);
    this.tabMessaging.showEdit(+sf!);
  }

  loadDevices(id:number)
  {
    this.isLoading = true;
    this.safetyDocumentService.getSafetyDocumentDevices(id).subscribe(
      data =>{
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
          {
            this.loadDevices(id);
          }else
          {
            this.toastr.error(error.error);
          }
      }
    )
  }

}



