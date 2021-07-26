import { Location } from "./location.model";

export class Device{

    id:number = 0;
    name:string="";
    deviceType:string="";
    locationId:number=0;
    location:Location;
    timestamp: Date;
    
}