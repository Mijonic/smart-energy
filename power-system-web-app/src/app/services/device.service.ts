import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Device } from 'app/shared/models/device.model';
import { DeviceList } from 'app/shared/models/devices-list.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private http: HttpClient) { }
  
  getAllDevices():Observable<Device[]>{
    let requestUrl = environment.deviceServerURL.concat("devices/all");
    return this.http.get<Device[]>(requestUrl);
  }

  getDevicesPaged( page: number, perPage:number,sort?: string, order?: string):Observable<DeviceList>{
    let requestUrl = environment.deviceServerURL.concat("devices");
    
   
    
    let params = new HttpParams();
    if(sort)
      params = params.append('sortBy', sort);
    if(order)
      params = params.append('direction', order);

    params = params.append('page', page.toString());
    params = params.append('perPage', perPage.toString());

  
    return this.http.get<DeviceList>(requestUrl, {params:params});
  }


  getSearchDevicesPaged( page: number, perPage:number,sort?: string, order?: string, type?: string, field?:string, searchParam?: string ):Observable<DeviceList>{
    let requestUrl = environment.deviceServerURL.concat("devices/search");
      
    let params = new HttpParams();
    if(sort)
      params = params.append('sortBy', sort);
    if(order)
      params = params.append('direction', order);

    params = params.append('page', page.toString());
    params = params.append('perPage', perPage.toString());

    if(type)
      params = params.append('type', type);

    if(field)
      params = params.append('field', field);

    if(searchParam)
    params = params.append('searchParam', searchParam);

  
    return this.http.get<DeviceList>(requestUrl, {params:params});
  }


  getDeviceById(id:number):Observable<Device>{
    let requestUrl = environment.deviceServerURL.concat(`devices/${id}`);
    return this.http.get<Device>(requestUrl);
  }

  createNewDevice(device:Device):Observable<Device>{
    console.log(device)
    let requestUrl = environment.deviceServerURL.concat("devices");
    return this.http.post<Device>(requestUrl, device);
  }

  updateDevice(device: Device):Observable<Device>{
    let requestUrl = environment.deviceServerURL.concat(`devices/${device.id}`);
    return this.http.put<Device>(requestUrl, device);
  }

  deleteDevice(id:number):Observable<{}>{
    let requestUrl = environment.deviceServerURL.concat(`devices/${id}`);
    return this.http.delete(requestUrl);
  }

}
