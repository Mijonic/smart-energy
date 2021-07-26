import { Location } from "./location.model";
export class User {
    id:number = 0;
    name:string;
    lastname:string;
    birthDay:Date;
    email:string;
    username:string;
    location:Location;
    userType:string;
    userStatus:string;
    crewID:number;
    imageURL:string;
    password:string;
}

