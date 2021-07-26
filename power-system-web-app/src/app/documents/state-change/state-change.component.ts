import { DisplayService } from './../../services/display.service';
import { StateChange } from './../../shared/models/state-change.model';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-state-change',
  templateUrl: './state-change.component.html',
  styleUrls: ['./state-change.component.css']
})
export class StateChangeComponent implements OnInit {
  @Input()
  stateChange:StateChange;

  constructor(public display:DisplayService) { }

  ngOnInit(): void {
  }

}
