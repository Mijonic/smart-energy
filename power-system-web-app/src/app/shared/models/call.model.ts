import { Consumer } from "./consumer.model";
import { Location } from "./location.model";

export class Call {
    id:number = 0;
    callReason:string="";
    comment:string="";
    hazard:string="";
    locationId: number;
    location: Location;
    consumerId: number;
    consumer: Consumer
    incidentId: number;
   
}

