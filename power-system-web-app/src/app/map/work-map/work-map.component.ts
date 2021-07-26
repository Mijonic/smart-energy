import { AuthGuardService } from './../../auth/auth-guard.service';
import { DisplayService } from './../../services/display.service';
import { DeviceService } from './../../services/device.service';
import { ToastrService } from 'ngx-toastr';
import { IncidentMapDisplay } from './../../shared/models/incident-map-display.model';
import { IncidentService } from './../../services/incident.service';
import { Component, OnInit, AfterViewInit, HostListener, ViewChild, NgZone } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as L from 'leaflet';
import { Device } from 'app/shared/models/device.model';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = iconDefault;

@Component({
  selector: 'app-work-map',
  templateUrl: './work-map.component.html',
  styleUrls: ['./work-map.component.css'],
})
export class WorkMapComponent implements OnInit, AfterViewInit {
  private map!: L.Map;
  public innerHeight: any;
  public containerHeight!:number;
  private hazardIcon:any;
  private crewIcon:any;
  private deviceIcon:any;
  ngZone:any;
  isLoading:boolean = true;
  incidents:IncidentMapDisplay[] = [];     
  devices:Device[] = [];        
  zoomDeviceID:number = -1;

  
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.innerHeight = window.innerHeight;
    this.containerHeight = this.innerHeight - 76;
  }

  constructor(private _router: Router, ngZone:NgZone, private incidentService:IncidentService, private deviceService:DeviceService,
     private toastr:ToastrService, private displayService:DisplayService, private route:ActivatedRoute, private _authGuard:AuthGuardService) {
    this.ngZone = ngZone;
   } 

  ngAfterViewInit(): void {
  }

  loadDevices()
  {
    this.deviceService.getAllDevices().subscribe(
      data =>{
        this.devices = data;
        this.addDeviceMarkers(data);
      },
      error=>{
        this.toastr.error("Cannot load devices","", {positionClass: 'toast-bottom-left'}); 
     }
    )
  }

  loadIncidents()
  {
    this.incidentService.getUnresolvedIncidents().subscribe(
      data=>{
          this.incidents = data;
          this.addIncidentCrewMarkers(data);
      },
      error=>{
         this.toastr.error("Cannot load incidents","", {positionClass: 'toast-bottom-left'}); 
      }
    )
  }

  ngOnInit(): void {
    const deviceID = this.route.snapshot.paramMap.get('deviceid');
    if(deviceID && deviceID != "")
    {
      this.zoomDeviceID = +deviceID;
    }
    this.initMap();
    this.defineIcons();
    if(!this._authGuard.isUserConsumer())
    {
      this.loadDevices();
    }
      
    this.loadIncidents();
    window.dispatchEvent(new Event('resize'));
  }

  private initMap(): void {
    this.map = L.map('map', {
      center: [ 44.2107675, 20.9224158],
      zoom: 8
    });

    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxNativeZoom:19,
    maxZoom: 22,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(this.map); 
  }

  private defineIcons():void{
    let iconUrl = '../../assets/Images/crew-icon.png';
    this.crewIcon = L.icon({
      iconUrl,
      iconSize: [41, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });

    iconUrl = '../../assets/Images/hazard-icon.png';
    this.hazardIcon = L.icon({
      iconUrl,
      iconSize: [50, 50],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });

    iconUrl = '../../assets/Images/device.png';
    this.deviceIcon = L.icon({
      iconUrl,
      iconSize: [41, 21],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });
  }

  private addDeviceMarkers(devices:Device[])
  {
    let processedLocations = new Map<number, number>();
    devices.forEach(device=> {

      if(processedLocations.has(device.location.id))
      {
        let numberOfDevices =  processedLocations.get(device.location.id)!;
        device.location.latitude += numberOfDevices * 0.00001;
        device.location.longitude += numberOfDevices * 0.00001;
        processedLocations.set(device.location.id, numberOfDevices++);
      }else
      {
        processedLocations.set(device.location.id, 1);
      }
      let marker = L.marker([device.location.latitude, device.location.longitude], {icon:this.deviceIcon});
      marker.bindTooltip(`Name: ${device.name} <br/>
                          Type: ${device.deviceType} <br/>
                          Address: ${device.location.street} ${device.location.number}`);
      /*marker.on('click', (e) => {
        this._router.navigate(['/']);
      });*/
      marker.addTo(this.map);  

      if(this.zoomDeviceID == device.id)
      {
        this.map.flyTo([ device.location.latitude, device.location.longitude], 22) 
      }

    });
  }

  private addIncidentCrewMarkers(incidentDisplay:IncidentMapDisplay[])
  {
    let processedLocations = new Map<number, number>();
    incidentDisplay.forEach(inc=> {
      if(processedLocations.has(inc.location.id))
      {
        let numberOfIncidents =  processedLocations.get(inc.location.id)!;
        inc.location.latitude += numberOfIncidents * 0.00005;
        inc.location.longitude += numberOfIncidents * 0.00001;
        processedLocations.set(inc.location.id, numberOfIncidents++);
      }else
      {
        processedLocations.set(inc.location.id, 1);
      }
      let marker = L.marker([inc.location.latitude, inc.location.longitude], {icon:this.hazardIcon});
      marker.bindTooltip(`Incident<br>
                          Date: ${this.displayService.getDateDisplay(inc.incidentDateTime)} <br/>
                          Priority: ${inc.priority} <br/>
                          Address: ${inc.location.street} ${inc.location.number}`);
      marker.on('click', (e) => {
        this._router.navigate(['/incident/basic-info', inc.id]);
      });
      marker.addTo(this.map);  
      if(this._authGuard.isUserConsumer())
        return;
      if(!inc.crew)
        return;

      let crewMarker = L.marker([inc.location.latitude + 0.00002, inc.location.longitude], {icon:this.crewIcon});
      crewMarker.bindTooltip(`Crew<br>
                              Name: ${inc.crew.crewName}`);
      crewMarker.on('click', (e) => {
        this._router.navigate(['/incident', inc.id]);
      });
      crewMarker.addTo(this.map);  
    });
  }

}
