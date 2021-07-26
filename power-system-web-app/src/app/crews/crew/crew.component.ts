import { ValidationService } from './../../services/validation.service';
import { CrewService } from './../../services/crew.service';
import { UserService } from './../../services/user.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { Crew } from 'app/shared/models/crew.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-crew',
  templateUrl: './crew.component.html',
  styleUrls: ['./crew.component.css']
})
export class CrewComponent implements OnInit {
  unassignedCrewMembers :User[] = [];
  crew:Crew = new Crew();
  crewForm = new FormGroup({
  crewNameControl : new FormControl('', Validators.required)
  });
  showCrewError:boolean = false;
  isNew:boolean = true;
  isLoadingUnassigned:boolean = true;
  isLoadingCrew:boolean = true;

  constructor(private userService:UserService, private crewService:CrewService, private route:ActivatedRoute, 
              private router:Router,private toastr: ToastrService, private validationService:ValidationService){}
  ngOnInit(): void {
    this.isLoadingCrew = true;
    this.isLoadingUnassigned = true;
    this.loadUnassignedCrew();

    const crewId = this.route.snapshot.paramMap.get('id');
    if(crewId && crewId != "")
    {
      this.isNew = false;
      this.loadCrew(+crewId);
    }else
    {
      this.isLoadingCrew = false;
    }
  }

  loadCrew(id:number){
    this.crewService.getCrewById(id).subscribe(
      data =>{
        this.crew = data;
        this.crewForm.controls['crewNameControl'].setValue(data.crewName);
        this.isLoadingCrew = false;
      },
      error =>{
        this.toastr.error('Could not load crew.',"", {positionClass: 'toast-bottom-left'})
        console.log(error.error);
        this.isLoadingCrew = false;
      }
    );
  }

  loadUnassignedCrew()
  {
    this.userService.getAllUnassignedCrewMembers().subscribe( 
      data=> {
      this.unassignedCrewMembers = data;
      this.isLoadingUnassigned = false;
     },
     error =>{
      this.loadUnassignedCrew();
     }
    );

  }

  saveChanges()
  {
    this.showCrewError = true;
    if(this.crewForm.valid && this.crew.crewMembers.length > 0)
    {
      this.crew.crewName = this.crewForm.controls['crewNameControl'].value;
      if(this.isNew)
      {
        this.crewService.createNewCrew(this.crew).subscribe(
          data => {
            this.toastr.success("Crew created successfully","", {positionClass: 'toast-bottom-left'});
            this.router.navigate(['crews']);
            },
            error=>{
              this.toastr.error(error.error);
            }
        );
      }else
      {
        this.crewService.updateCrew(this.crew).subscribe(
            data => {
            this.crew = data;
            this.toastr.success("Crew updated successfully","", {positionClass: 'toast-bottom-left'});
            this.router.navigate(['crews']);
            },
            error=>{
              this.toastr.error(error.error);
            }
        );
      }
    }
    else{
        this.validationService.validateAllFields(this.crewForm);
    }
  }

  drop(event: CdkDragDrop<User[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }

  addMember(member:User)
  {
    transferArrayItem(this.unassignedCrewMembers,
      this.crew.crewMembers,
      this.unassignedCrewMembers.indexOf(member),
      0);

  }

  removeMember(member:User)
  {
    transferArrayItem(this.crew.crewMembers,
      this.unassignedCrewMembers,
      this.crew.crewMembers.indexOf(member),
      0);
  }

}
