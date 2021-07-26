export class Incident{

    id:number = 0;
    priority:number = 0;
    confirmed:boolean;
    eta:Date;
    ata:Date;
    etr:Date;
    workBeginDate:Date;
    incidentDateTime:Date;
    voltageLevel:number;
    description:string;
    userId:number;
    crewId:number;
    workType:string;
    incidentStatus:string;
    timestamp:Date;
}