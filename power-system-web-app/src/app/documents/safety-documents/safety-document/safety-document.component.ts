import { Component, OnDestroy, OnInit } from '@angular/core';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-safety-document',
  templateUrl: './safety-document.component.html',
  styleUrls: ['./safety-document.component.css']
})
export class SafetyDocumentComponent implements OnInit, OnDestroy {


  isNew:boolean = true;
  tabMessagingSubscription!:Subscription;

  navLinks = [
    { path: 'basic-info', label: 'Basic information', isDisabled: false },
    { path: 'state-changes', label: 'History of state changes', isDisabled: this.isNew },
    { path: 'multimedia', label: 'Mutlimedia attachments', isDisabled: this.isNew },
    { path: 'equipment', label: 'Equipment', isDisabled: this.isNew },
    { path: 'checklist', label: 'Checklist', isDisabled: this.isNew },
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
