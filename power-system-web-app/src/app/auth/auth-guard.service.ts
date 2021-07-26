import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'app/shared/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router, private toastr:ToastrService) {
  }
  canActivate(route:ActivatedRouteSnapshot) {
    const token = localStorage.getItem("jwt");

    if(!token)
    {
      this.toastr.warning("Please log in.","", {positionClass: 'toast-bottom-left'});
      this.router.navigate(["/"]);
      return false;
    }

    if (this.jwtHelper.isTokenExpired(token)){


      this.toastr.warning("Your session has expired, please log in again.","", {positionClass: 'toast-bottom-left'});
      this.router.navigate(["/"]);
      return false;
    }


    if (route.data && route.data.roles) {
      let roles:string[] = route.data.roles;
      let user:User = JSON.parse(localStorage.getItem("user")!);
      if(roles.indexOf(user.userType) == -1)
      {
        this.toastr.warning("Your are not authorized to view this.","", {positionClass: 'toast-bottom-left'});
        this.router.navigate(["/dashboard"]);
        return false;
      }
    }

    return true;

  }

  isUserApproved()
  {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    return user.userStatus == "APPROVED";
  }

  isUserAdmin()
  {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    return user.userType == "ADMIN";

  }

  isUserDispatcher()
  {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    return user.userType == "DISPTACHER";

  }

  isUserWorker()
  {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    return user.userType == "WORKER";

  }

  isUserConsumer()
  {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    return user.userType == "CONSUMER";

  }

  isUserCrew()
  {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    return user.userType == "CREW_MEMBER";

  }
}
