import { User } from "./user.model";

export class LoginResponse{
    token:string;
    isSuccessfull:boolean;
    user:User;
}