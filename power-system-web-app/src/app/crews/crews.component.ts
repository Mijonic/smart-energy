import { ToastrService } from 'ngx-toastr';
import { MatSort } from '@angular/material/sort';
import { Crew } from 'app/shared/models/crew.model';
import { CrewService } from './../services/crew.service';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { merge, Observable, of } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-crews',
  templateUrl: './crews.component.html',
  styleUrls: ['./crews.component.css']
})
export class CrewsComponent implements OnInit, AfterViewInit{
  displayedColumns: string[] = ['action', 'crewName', 'members'];
  dataSource: MatTableDataSource<Crew>;
  filteredAndPagedCrews: Observable<Crew[]>;
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private crewService:CrewService, private toastr:ToastrService) {
  }
  ngAfterViewInit(): void {
    if(this.isLoading)
      this.loadCrews();
  }

  ngOnInit(): void {
      
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

  delete(crewId:number)
  {
    this.crewService.deleteCrew(crewId).subscribe(
      data =>{
        this.toastr.success("Crew deleted successfully.","", {positionClass: 'toast-bottom-left'});
        this.loadCrews();
    },
    error => {
        this.toastr.error(error.error);
    });
  }

  resetPaging(): void {
    this.paginator.pageIndex = 0;
  }

}


