import { Component, OnDestroy, OnInit } from '@angular/core';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.css']
})


 
export class IncidentComponent implements OnInit, OnDestroy {

  isNew:boolean = true;
  tabMessagingSubscription!:Subscription;

  navLinks = [
    { path: 'basic-info', label: 'Basic information', isDisabled: false  },
    { path: 'calls', label: 'Calls', isDisabled: this.isNew  },
    { path: 'crew', label: 'Crew', isDisabled: this.isNew   },
    { path: 'multimedia', label: 'Multimedia attachments', isDisabled: this.isNew  },
    { path: 'devices', label: 'Devices', isDisabled: this.isNew  },
    { path: 'resolution', label: 'Resolution', isDisabled: this.isNew  },

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
