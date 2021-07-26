import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Settings } from 'app/shared/models/settings.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient) { }

  getLastSetting():Observable<Settings>{
    let requestUrl = environment.serverURL.concat("settings/last");
    return this.http.get<Settings>(requestUrl);
  }

  updateSettings(settings:Settings):Observable<Settings>{
    let requestUrl = environment.serverURL.concat(`settings/${settings.id}`);
    return this.http.put<Settings>(requestUrl, settings);
  }

  resetSettings():Observable<{}>{
    let requestUrl = environment.serverURL.concat(`settings`);
    return this.http.delete<Settings>(requestUrl);
  }
}
