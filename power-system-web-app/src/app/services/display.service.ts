import { Location } from './../shared/models/location.model';
import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DisplayService {

  constructor(private datePipe:DatePipe) { }

  getBoolDisplay(bool:boolean)
  {
     if(bool)
      return "Yes";

      return "No";
  }

  getDateDisplay(date:Date)
  {
    return this.datePipe.transform(date, "dd/MM/yyyy");
  }

  getDateDisplayIncident(date:Date)
  {
    return this.datePipe.transform(date, "M/d/yy, h:mm a");
  }

  getTimeDisplayIncidentETR(dateETR: string)
  {

    let date: Date = new Date(dateETR);

    return date.getHours() + " h, " + date.getMinutes() + " min";
  
  }

  getStateChangeDateDisplay(date:Date)
  {
    return this.datePipe.transform(date, "dd/MM/yyyy hh:mm");
  }
  

  getCoordinatesDisplay(location:Location){
    return `${location.latitude}, ${location.longitude}`;
  }

  getAddressDisplay(location:Location){
    return `${location.street} ${location.number}, ${location.city}`;
  }
}
