import { User } from "./user.model";

export class SafetyDocument {
    id:number = 0;
    details:string;
    notes:string;
    phone:string;

    operationCompleted:boolean;
    tagsRemoved:boolean;
    groundingRemoved:boolean;
    ready:boolean;


    createdOn:Date;
    documentType:string;
    documentStatus:string;
    userID:number = 0;
    user: User
    workPlanID:number = 0;

    crewName: string;





}



       
    
       