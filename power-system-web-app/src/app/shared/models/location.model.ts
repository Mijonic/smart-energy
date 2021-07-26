import { User } from "./user.model";

export class Location {
    id:number = 0;
    number:number = 0;
    street:string;
    city:string;
    zip:string;
    morningPriority:number;
    noonPriority:number;
    nightPriority:number;
    latitude:number;
    longitude:number;
    users:User[];
}
