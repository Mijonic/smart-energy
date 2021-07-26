import { TabMessagingService } from './../../../services/tab-messaging.service';
import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-work-request',
  templateUrl: './work-request.component.html',
  styleUrls: ['./work-request.component.css']
})
export class WorkRequestComponent implements OnInit, OnDestroy{
  isNew:boolean = true;
  tabMessagingSubscription!:Subscription;
  navLinks = [
    { path: 'basic-info', label: 'Basic information', isDisabled: false },
    { path: 'state-changes', label: 'History of state changes', isDisabled: this.isNew },
    { path: 'multimedia', label: 'Multimedia attachments', isDisabled: this.isNew },
    { path: 'equipment', label: 'Equipment', isDisabled: this.isNew },
  ];

  

  constructor(private tabMessaging:TabMessagingService) { }

  ngOnDestroy(): void {
    this.tabMessagingSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.tabMessagingSubscription = this.tabMessaging.getMessage().subscribe( message => {
      if(this.isNew)
        this.showEdit(message);
    });
  }

  showEdit(id:any)
  {
    this.isNew = false;
    this.navLinks.forEach( f => {
      f.path = f.path.concat(`/${id}`);
      f.isDisabled = false;
  });
  }

}
