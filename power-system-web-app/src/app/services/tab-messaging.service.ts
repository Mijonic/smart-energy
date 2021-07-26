import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TabMessagingService implements OnDestroy{
  private subject = new Subject<any>();
  
  constructor() { }

  ngOnDestroy(): void {
    this.subject.unsubscribe();
  }

  showEdit(id :number)
  {
    this.subject.next(id);
  }

  getMessage():Observable<any>{
    return this.subject.asObservable();
  }

}
