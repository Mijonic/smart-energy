import { FormGroup, FormControl } from '@angular/forms';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  roles: any[] = 
  [ {role:'all', value:'All'},
    {role:'worker', value:'Worker'},
    {role:'admin', value:"Administrator"},
    {role:'dispatcher', value:'Dispatcher'},
    {role:'crew_member', value:'Crew member'},
    ];
  users:User[] = [];
  allUsers:User[] = [];
  isLoading:boolean = true;
  currentPage:number = 0;
  perPage:number = 5;
  totalResultCount:number = 0;

  usersForm = new FormGroup({
      search:new FormControl(""),
      typeFilter:new FormControl("all"),
      statusFilter:new FormControl("all"),
  });

  constructor(private userService:UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.getUsers(this.currentPage, this.perPage);
  }

  shouldLoadMore()
  {
    return this.totalResultCount > (this.currentPage + 1) * this.perPage;
  }

  getUsers(currentPage:number = 0, perPage:number = 5)
  {
    this.isLoading = true;
    let type:string = this.usersForm.controls['typeFilter'].value;
    let status:string = this.usersForm.controls['statusFilter'].value;
    let search:string = this.usersForm.controls['search'].value;
    this.userService.getUsersPaged(currentPage, perPage, undefined, undefined, type, search, status).subscribe(
      data =>{
        this.currentPage = currentPage;
        this.perPage = perPage;
        if(currentPage > 0)
        {
          this.users = this.users.concat(data.users);
        }else
        {
          this.users = data.users;
        }

        this.totalResultCount = data.totalCount;
        this.isLoading = false;
      }
    )
  }

  loadMoreUsers()
  {
    this.currentPage += 1;
    this.getUsers(this.currentPage, this.perPage);
  }


}
