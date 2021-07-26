import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { IncidentService } from 'app/services/incident.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { Call } from 'app/shared/models/call.model';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-calls',
  templateUrl: './calls.component.html',
  styleUrls: ['./calls.component.css']
})
export class CallsComponent implements  OnInit {
  displayedColumns: string[] = ['id', 'callReason', 'hazard', 'comment', 'name', 'lastname'];
  reasons:string[]=['ALL', 'NO_POWER', 'FLICKERING_LIGHT', 'PARTIAL_POWER', 'VOLTAGE_PROBLEM', 'POWER_RESTORED', 'MALFUNCTION'];
  

  filterCalls = new FormGroup({
     
    reasonsControl: new FormControl('')  

  });

 
  dataSource: MatTableDataSource<Call>;
  showAllCalls:boolean = true;

  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  calls:Call[] = [];
  allCalls:Call[] = [];

  incidentId: number = 0;





  constructor(private router:Router, private tabMessaging:TabMessagingService, private route:ActivatedRoute,private incidentService:IncidentService,  private toastr: ToastrService) {
   
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    console.log(event)
  }

 

  ngOnInit(): void {

    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);
    
      this.incidentId = +incidentId;
      this.loadIncidentCalls();
    }
    
  }

  filter()
  {
    if(this.filterCalls.controls['reasonsControl'].value == "ALL")
    { 
        this.dataSource.filter = "";
    }else
    {
      this.dataSource.filter = this.filterCalls.controls['reasonsControl'].value 
    }
    
  }


  
  loadIncidentCalls()
  {
    this.incidentService.getIncidentCalls(this.incidentId).subscribe(
      data =>{
        this.allCalls = data;
        this.calls = data;
        
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = false;

      

       
       
      },
      error =>{


        if(error.error instanceof ProgressEvent)
        {
          this.loadIncidentCalls();

        }else
        {
          this.toastr.error('Could not load incident calls.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }

        
      }
    )
  }



  onAddClick()
  {
    this.showAllCalls = false;
  }

  showCalls(){

    this.loadIncidentCalls();
    this.showAllCalls = true;
  }

}
