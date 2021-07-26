import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabMessagingService } from 'app/services/tab-messaging.service';






@Component({
  selector: 'app-multimedia-attachments',
  templateUrl: './multimedia-attachments.component.html',
  styleUrls: ['./multimedia-attachments.component.css']
})
export class MultimediaAttachmentsComponent implements OnInit {


  
  constructor(private tabMessaging:TabMessagingService, private route:ActivatedRoute) { }

  ngOnInit(): void {

    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);
     // this.isNew = false;
      //this.workReqId = +wrId;
     /// this.loadWorkRequest(this.workReqId);
    }

    window.dispatchEvent(new Event('resize'));
    
    
  }

}
  

