import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DisplayService } from 'app/services/display.service';
import { IncidentService } from 'app/services/incident.service';
import { Incident } from 'app/shared/models/incident.model';
import { User } from 'app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';
import { merge, Observable, of } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';






@Component({
  selector: 'app-incidents',
  templateUrl: './incidents.component.html',
  styleUrls: ['./incidents.component.css']
})
export class IncidentsComponent implements OnInit, AfterViewInit  {

  displayedColumns: string[] = ['action', 'id', 'type', 'priority', 'confirmed', 'status', 'ETA', 'ATA', 'incidentDateTime', 'ETR', 'voltageLevel', 'plannedWork', 'solveIncident' ];
  //dataSource: MatTableDataSource<Incident>;

  dataSource:Observable<Incident[]>;

  filterIncident: any[] = 
  [ 
    {filter:'ALL', value:'ALL'},
    {filter:'CONFIRMED', value:'CONFIRMED'},
    {filter:'UNRESOLVED', value:'UNRESOLVED'},
    {filter:'RESOLVED', value:"RESOLVED"},
    {filter:'INITIAL', value:'INITIAL'},
    {filter:'PLANNED', value:'PLANNED'},
    {filter:'UNPLANNED', value:'UNPLANNED'},
  ];

  isLoading:boolean = true;
  incidentForm = new FormGroup(
    {
      searchControl:new FormControl(''),
      filterControl:new FormControl('all'),
      documentOwnerControl:new FormControl('all')
    }
  );


  


  incidents:Incident[] = [];
  allIncidents:Incident[] = [];
  user: User = new User();
  

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private incidentService:IncidentService,  private toastr: ToastrService, public display:DisplayService) {

  }

  ngOnInit(): void {
    window.dispatchEvent(new Event('resize'));
    //this.getIncidents();

    this.user = JSON.parse(localStorage.getItem("user")!);
    
    
  }

  ngAfterViewInit(): void {
    if(this.isLoading)
      this.getIncidentsPaged();
  }



  getIncidentsPaged() {

    let filter = this.incidentForm.controls['filterControl'].value;
    let owner = this.incidentForm.controls['documentOwnerControl'].value;
    let search = this.incidentForm.controls['searchControl'].value;

    this.dataSource = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.incidentService.getIncidentsPaged(
             this.paginator.pageIndex, this.paginator.pageSize, this.sort.active, this.sort.direction, filter, search, owner );
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoading = false;
          this.paginator.length = data.totalCount;

          return data.incidents;
        }),
        catchError(() => {
          this.isLoading = false;
          return of([]);
        })
      );
  }


 
  // getIncidents()
  // {
  //   this.incidentService.getAllIncidents().subscribe(
  //     data =>{
  //       this.allIncidents = data;
  //       this.incidents = data;
  //       this.dataSource = new MatTableDataSource(data);
  //       this.isLoading = false;

  //       console.log(this.allIncidents);
       
  //     },
  //     error =>{

  //       this.getIncidents();
  //     }
  //   )
  //   }

 
  resetPaging(): void {
    this.paginator.pageIndex = 0;
  }

  reload()
  {
    this.getIncidentsPaged();
  }
  

  delete(incidentId: number)
  {
   
    this.incidentService.deleteIncident(incidentId).subscribe(x =>{
        this.getIncidentsPaged();
        this.toastr.success("Incident successfully deleted","", {positionClass: 'toast-bottom-left'});
    });
  }

  assignIncidentToUser(incidentId: number)
  {
    this.incidentService.assignIncidetToUser(incidentId, +this.user.id).subscribe(
      data =>{
        
        this.toastr.success('Incicent succesfully assigned to you',"", {positionClass: 'toast-bottom-left'})
       
       
        
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

  

}


