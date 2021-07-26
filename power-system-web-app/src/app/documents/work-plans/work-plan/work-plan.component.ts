import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-work-plan',
  templateUrl: './work-plan.component.html',
  styleUrls: ['./work-plan.component.css']
})
export class WorkPlanComponent implements OnInit {
  navLinks = [
    { path: 'basic-info', label: 'Basic information' },
    { path: 'equipment', label: 'Equipment' },
    { path: 'state-changes', label: 'History of state changes' },
    { path: 'switching-instructions', label: 'Switching instructions' },
    { path: 'multimedia', label: 'Multimedia attachments' },

  ];

  constructor() { }

  ngOnInit(): void {
  }

}
