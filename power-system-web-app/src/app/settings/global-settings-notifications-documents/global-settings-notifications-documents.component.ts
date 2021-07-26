import { FormGroup, FormControl } from '@angular/forms';
import { Settings } from './../../shared/models/settings.model';
import { ToastrService } from 'ngx-toastr';
import { SettingsService } from './../../services/settings.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-global-settings-notifications-documents',
  templateUrl: './global-settings-notifications-documents.component.html',
  styleUrls: ['./global-settings-notifications-documents.component.css']
})
export class GlobalSettingsNotificationsDocumentsComponent implements OnInit {
  settings:Settings = new Settings();


  constructor(private settingsService:SettingsService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.loadSettings();
  }

  loadSettings()
  {
    this.settingsService.getLastSetting().subscribe(
      data =>{
        this.settings = data;
      }
    )
  }

  onSave(){
    this.settingsService.updateSettings(this.settings).subscribe(
      data =>{
        this.settings = data;
        this.toastr.success("Settings updated successfully");
      },
      error =>{
        this.toastr.error(error.error);
      }
    )
  }

}
