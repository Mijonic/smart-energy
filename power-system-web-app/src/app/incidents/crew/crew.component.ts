import { ContentObserver } from '@angular/cdk/observers';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { CrewService } from 'app/services/crew.service';
import { IncidentService } from 'app/services/incident.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { Crew } from 'app/shared/models/crew.model';
import { ToastrService } from 'ngx-toastr';
import { merge, Observable, of } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';



@Component({
  selector: 'app-crew',
  templateUrl: './crew.component.html',
  styleUrls: ['./crew.component.css']
})
export class IncidentCrewComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['id', 'crewName', 'members', 'select'];
  dataSource: MatTableDataSource<Crew>;
  filteredAndPagedCrews: Observable<Crew[]>;
  isLoading:boolean = true;

  incidentCrew: Crew = new Crew();
  incidentId: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private crewService:CrewService, private tabMessaging:TabMessagingService, private route:ActivatedRoute, private router:Router, private toastr:ToastrService, private incidentService: IncidentService) {
  }
  ngAfterViewInit(): void {
    if(this.isLoading)
      this.loadCrews();
  }

  ngOnInit(): void {
    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);
      this.incidentId = +incidentId;
      this.loadCrew(this.incidentId);
    }

    window.dispatchEvent(new Event('resize'));
  }


  addCrewToIncident(crewId: number)
  {
    
    this.incidentService.addCrewToIncident(this.incidentId, crewId).subscribe(
      data =>{

        this.loadCrew(this.incidentId);
        this.isLoading = false;
        this.toastr.success('Crew assigned to incident successfully',"", {positionClass: 'toast-bottom-left'})
        
       
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.addCrewToIncident(crewId);

        }else
        {
          this.toastr.error('Could not assign crew to incident.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
      }
    )
  }


  removeCrewFromIncidet()
  {
    

    this.incidentService.removeCrewFromIncidet(this.incidentId).subscribe(
      data =>{

        this.loadCrew(this.incidentId);
        this.isLoading = false;
        this.toastr.success('Crew unassigned from incident successfully',"", {positionClass: 'toast-bottom-left'})
        
       
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.removeCrewFromIncidet();

        }else
        {
          this.toastr.error('Could not unassign crew from incident.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
      }
    )
  }



  loadCrew(incidentId: number)
  {
    this.isLoading = true;
    this.incidentService.getIncidentCrew(incidentId).subscribe(
      data =>{
        this.isLoading = false;
        this.incidentCrew = data;
        
      } ,
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.loadCrew(incidentId);
        }else
        {
          this.toastr.error('Could not load incident crew.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
      }
    );
  }



  loadCrews() {

    this.filteredAndPagedCrews = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.crewService.getCrewsPaged(
            this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoading = false;
          this.paginator.length = data.totalCount;

          return data.crews;
        }),
        catchError(() => {
          this.isLoading = false;
          return of([]);
        })
      );
  }

 
 

  resetPaging(): void {
    this.paginator.pageIndex = 0;
  }
}






