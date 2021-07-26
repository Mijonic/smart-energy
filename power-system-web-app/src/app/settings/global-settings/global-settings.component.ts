import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-global-settings',
  templateUrl: './global-settings.component.html',
  styleUrls: ['./global-settings.component.css']
})
export class GlobalSettingsComponent implements OnInit {
  navLinks = [
    { path: 'change-password', label: 'Change password' },
    { path: 'icons', label: 'Icons' },
    { path: 'notifications-documents', label: 'Notifications & documents' },
    { path: 'reset-default', label: 'Reset default' },
    { path: 'streets-priority', label: 'Streets priority' },
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
