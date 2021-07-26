import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  @Input()
  accountStatus: string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
