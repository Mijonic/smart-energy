import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SettingsService } from './../../services/settings.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-global-settings-reset-default',
  templateUrl: './global-settings-reset-default.component.html',
  styleUrls: ['./global-settings-reset-default.component.css']
})
export class GlobalSettingsResetDefaultComponent implements OnInit {

  constructor(private settingsService:SettingsService, private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
  }

  onReset(){
    this.settingsService.resetSettings().subscribe(
      data =>{
        this.toastr.success("Reset successfull.")
        this.router.navigate(['global-settings/notifications-documents']);
      }
    )
  }

}
