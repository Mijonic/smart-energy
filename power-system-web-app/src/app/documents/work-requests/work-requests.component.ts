import { WorkRequest } from 'app/shared/models/work-request.model';
import { UserService } from './../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { DisplayService } from './../../services/display.service';
import { WorkRequestService } from './../../services/work-request.service';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource} from '@angular/material/table';
import { merge, Observable, of } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';


@Component({
  selector: 'app-work-requests',
  templateUrl: './work-requests.component.html',
  styleUrls: ['./work-requests.component.css']
})
export class WorkRequestsComponent implements OnInit,  AfterViewInit {
  displayedColumns: string[] = ['action', 'id', 'type', 'status', 'incident', 'street', 'startdate', 'enddate', 'emergency','company', 'phoneno', 'creationdate'];
  dataSource:Observable<WorkRequest[]>;
  documentStatuses: any[] = 
  [ {status:'All', value:'all'},
    {status:'Draft', value:'draft'},
    {status:'Canceled', value:"canceled"},
    {status:'Approved', value:'approved'},
    {status:'Denied', value:'denied'},
    ];
  isLoading:boolean = true;
  workRequestsForm = new FormGroup(
    {
      search:new FormControl(''),
      documentStatus:new FormControl('all'),
      documentOwner:new FormControl('all')
    }
  );

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private workRequestService:WorkRequestService, public display:DisplayService, private toastr:ToastrService,
    private userService:UserService) {
  }
  ngAfterViewInit(): void {
    if(this.isLoading)
      this.getWorkRequests();
  }

  ngOnInit(): void {

  }

  getWorkRequests() {

    let status = this.workRequestsForm.controls['documentStatus'].value;
    let owner = this.workRequestsForm.controls['documentOwner'].value;
    let search = this.workRequestsForm.controls['search'].value;
    this.dataSource = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.workRequestService.getWorkrequestsPaged(
             this.paginator.pageIndex, this.paginator.pageSize, this.sort.active, this.sort.direction,status, search, owner );
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoading = false;
          this.paginator.length = data.totalCount;

          return data.workRequests;
        }),
        catchError(() => {
          this.isLoading = false;
          return of([]);
        })
      );
  }


  delete(id:number)
  {
    this.isLoading = true;
    this.workRequestService.deleteWorkRequest(id).subscribe(
      data =>{
        this.isLoading = false;
        this.getWorkRequests();
        this.toastr.success("Work request successfully deleted.","", {positionClass: 'toast-bottom-left'});
        this.toastr.info("All media attached to this work request is also deleted.","", {positionClass: 'toast-bottom-left'});
      },
      error =>{

        this.isLoading = false;
        if(error.error instanceof ProgressEvent)
                {
                  this.toastr.error("Server is unreachable","", {positionClass: 'toast-bottom-left'});
                }else
                {
                  this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'});
                }
        this.getWorkRequests();
      }
    );
  }

  resetPaging(): void {
    this.paginator.pageIndex = 0;
  }

  reload()
  {
    this.getWorkRequests();
  }
}

