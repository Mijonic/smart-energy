import { Crew } from './crew.model';
import { Location } from './location.model';
export class IncidentMapDisplay{
    id:number = 0;
    priority:number = 0;
    incidentDateTime:Date;
    location:Location;
    crew:Crew;
}