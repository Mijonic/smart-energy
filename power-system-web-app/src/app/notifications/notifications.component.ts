import { Component, OnInit } from '@angular/core';
import {Message,MessageService} from 'primeng/api';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css'],
  providers: [MessageService]
})

export class NotificationsComponent implements OnInit {

  msgs1: Message[];
  dates = ['11.08.1999', '21.02.2922.', '1.22.2222.'];
  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  
  constructor(private messageService: MessageService) 
  {
  }

  ngOnInit() {
      this.msgs1 = [
          {severity:'success', summary:'Success', detail:'Message Content'},
          {severity:'info', summary:'Info', detail:'Message Content'},
          {severity:'warn', summary:'Warning', detail:'Message Content'},
          {severity:'error', summary:'Error', detail:'Message Content'}
      ];
  }
  
}
