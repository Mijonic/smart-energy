import { DisplayService } from './../../../services/display.service';
import { ToastrService } from 'ngx-toastr';
import { IncidentService } from './../../../services/incident.service';
import { AfterViewInit, ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Incident } from 'app/shared/models/incident.model';

@Component({
  selector: 'app-choose-incident-dialog',
  templateUrl: './choose-incident-dialog.component.html',
  styleUrls: ['./choose-incident-dialog.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChooseIncidentDialogComponent implements OnInit {

  displayedColumns: string[] = ['action', 'type', 'priority', 'confirmed', 'status', 'incidentOccurred', 'voltageLevel'];
  dataSource: MatTableDataSource<Incident>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(public dialogRef: MatDialogRef<ChooseIncidentDialogComponent>, private incidentService:IncidentService, private toastr:ToastrService,
    public display:DisplayService) { }

   loadIncidents()
   {
     this.incidentService.getUnassignedIncidents().subscribe(
       data =>{
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
       },
       error =>{
        this.toastr.error(error.error);
        this.dialogRef.close();
       }
     )
   }

  
   ngOnInit(): void {
     this.loadIncidents();
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }


}


