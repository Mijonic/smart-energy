import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsumerService } from 'app/services/consumer.service';
import { IncidentService } from 'app/services/incident.service';
import { Consumer } from 'app/shared/models/consumer.model';
import { Location } from 'app/shared/models/location.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-select-consumer-dialog',
  templateUrl: './select-consumer-dialog.component.html',
  styleUrls: ['./select-consumer-dialog.component.css']
})
export class SelectConsumerDialogComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'lastname', 'accountID', 'accountType', 'add'];
  dataSource: MatTableDataSource<Consumer>;
  isLoading:boolean = true;

  accountTypes:string[]=['ALL', 'RESIDENTAL', 'NONRESIDENTAL'];
                                 
  searchFilterConsumers = new FormGroup({
     
    accountTypeControl: new FormControl(''),
    searchControl: new FormControl('')

  });

  incidentId: number;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  consumers:Consumer[] = [];
  allConsumers:Consumer[] = [];
 



  constructor(public dialogRef: MatDialogRef<SelectConsumerDialogComponent>, private consumerService:ConsumerService, private incidentService: IncidentService,  private toastr: ToastrService,
    private route:ActivatedRoute, private router:Router) {


  }


  
  filter()
  {

    this.dataSource.filter

    
    if(this.searchFilterConsumers.controls['accountTypeControl'].value == "ALL")
    { 
        if(this.searchFilterConsumers.controls['searchControl'].value != null && this.searchFilterConsumers.controls['searchControl'].value != "")
        {
          this.dataSource.filter = this.searchFilterConsumers.controls['searchControl'].value.trim().toLowerCase();
        }
        // this.dataSource.filter = "";
    }else
    {

      if(this.searchFilterConsumers.controls['searchControl'].value != null && this.searchFilterConsumers.controls['searchControl'].value != "")
      {
        this.dataSource.filter = this.searchFilterConsumers.controls['accountTypeControl'].value.trim().toLowerCase() + this.searchFilterConsumers.controls['searchControl'].value.trim().toLowerCase();
       
      }else
      {
        this.dataSource.filter = this.searchFilterConsumers.controls['accountTypeControl'].value.trim().toLowerCase();
      }

     
    }
    
  }

  

  ngOnInit(): void {
 

    window.dispatchEvent(new Event('resize'));

    this.getConsumers();
    this.isLoading = false;
    
    
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

 
  getConsumers()
  {
    this.consumerService.GetConsumers().subscribe(
      data =>{
        this.consumers = data;
        this.allConsumers = data;

        console.log(this.allConsumers)

        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = false;
       
      },
      error =>{


        if(error.error instanceof ProgressEvent)
        {
            this.getConsumers();
            this.isLoading = true;
        }else
        {
          this.toastr.error('Could not consumers.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.isLoading = false;
        }
       
     

    

      }
    )
  }

 


  onCancelClick(): void {
    this.dialogRef.close();
  }






}
