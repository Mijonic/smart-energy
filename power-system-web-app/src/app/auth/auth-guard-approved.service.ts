import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardApprovedService implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router, private toastr:ToastrService) {
  }
  canActivate(route:ActivatedRouteSnapshot) {
    let user:User = JSON.parse(localStorage.getItem("user")!);
    if(user.userStatus !== "APPROVED")
    {
      this.toastr.warning("Your need to be approved user to view this.","", {positionClass: 'toast-bottom-left'});
      this.router.navigate(["/dashboard"]);
      return false;
    }

    return true;
  }
}
