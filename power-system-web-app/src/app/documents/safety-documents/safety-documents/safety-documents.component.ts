
import { FormControl, FormGroup } from '@angular/forms';
import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';

import {MatTableDataSource} from '@angular/material/table';
import { SafetyDocument } from 'app/shared/models/safety-document.model';
import { SafetyDocumentService } from 'app/services/safety-document.service';
import { DisplayService } from 'app/services/display.service';
import { ToastrService } from 'ngx-toastr';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';




@Component({
  selector: 'app-safety-documents',
  templateUrl: './safety-documents.component.html',
  styleUrls: ['./safety-documents.component.css']
})
export class SafetyDocumentsComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['action','id', 'documentType', 'documentStatus', 'crewName', 'details', 'notes'];
  dataSource: MatTableDataSource<SafetyDocument>;

  toppings = new FormControl();
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato']; 
  isLoading:boolean = true;

  safetyDocuments:SafetyDocument[] = [];
  allsafetyDocuments:SafetyDocument[] = [];

  allMineForm = new FormGroup(
    {
      allMineControl:new FormControl('all'),   
    }
  );

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;



  constructor(private safetyDocumentService:SafetyDocumentService,  private toastr: ToastrService, public display:DisplayService) {

  }

  ngOnInit(): void {
    window.dispatchEvent(new Event('resize'));
    
    
    
  }

  ngAfterViewInit() : void{
  
   
    this.getSafetyDocuments();
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

 
  // getSafetyDocuments()
  // {

  //   console.log(this.allMineForm.controls['allMineControl'].value);
  //   this.safetyDocumentService.getAllSafetyDocuments().subscribe(
  //     data =>{
  //       this.allsafetyDocuments = data;
  //       this.safetyDocuments = data;
  //       this.dataSource = new MatTableDataSource(data);
  //       this.dataSource.paginator = this.paginator;
  //       this.dataSource.sort = this.sort;

  //       this.isLoading = false;

       
       
  //     },
  //     error =>{

  //       this.toastr.error("Unable to get safety documents","", {positionClass: 'toast-bottom-left'});
  //     }
  //   )
  // }


  getSafetyDocuments()
  {
    this.isLoading = true;

    let owner = this.allMineForm.controls['allMineControl'].value;
    this.safetyDocumentService.getAllMineSafetyDocuments(owner).subscribe(
      data =>{
        this.allsafetyDocuments = data;
        this.safetyDocuments = data;
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

        this.isLoading = false;

       
       
      },
      error =>{

        this.toastr.error("Unable to get safety documents","", {positionClass: 'toast-bottom-left'});
      }
    )
  }

  showStatus(status: string)
  {
    if(status== "APPROVED")
    {
       status = "ISSUED"
    }
    return status;
  }

  reload()
  {
    this.getSafetyDocuments();
  }
  


  

 

  
}

