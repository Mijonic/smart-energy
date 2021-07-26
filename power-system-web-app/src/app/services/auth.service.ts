import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from 'angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _http: HttpClient,  private _jwtHelper: JwtHelperService, private _externalAuthService: SocialAuthService) {}

  public signInWithGoogle = ()=> {
    return this._externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }
  public signInWithFacebook = ()=> {
    return this._externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }
  public signOutExternal = () => {
    return this._externalAuthService.signOut();
  }

  public signOut() {
    localStorage.removeItem("jwt");
    localStorage.removeItem("user");
    this.signOutExternal().catch( 
      err =>{

      });
  }
}
