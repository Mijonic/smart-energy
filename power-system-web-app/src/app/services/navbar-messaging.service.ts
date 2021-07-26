import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavbarMessagingService implements OnDestroy {

  private subject = new Subject<any>();
  
  constructor() { }
  ngOnDestroy(): void {
    this.subject.unsubscribe();
  }

  activateLogin()
  {
    this.subject.next("login");
  }

  activateRegister()
  {
    this.subject.next("Register");
  }


  getMessage():Observable<any>{
    return this.subject.asObservable();
  }
}
