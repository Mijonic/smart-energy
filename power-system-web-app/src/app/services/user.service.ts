import { LoginResponse } from './../shared/models/login-response.model';
import { ExternalAuth } from './../shared/models/external-auth.model';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'app/shared/models/login.model';
import { User } from 'app/shared/models/user.model';
import { UsersList } from 'app/shared/models/users-list.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  
  getAllUnassignedCrewMembers():Observable<User[]>{
    let requestUrl = environment.serverURL.concat("users/unassigned-crew-members");
    return this.http.get<User[]>(requestUrl);
  }

  getAllUsers():Observable<User[]>{
    let requestUrl = environment.serverURL.concat("users/all");
    return this.http.get<User[]>(requestUrl);
  }

  getUsersPaged( page: number, perPage:number,sort?: string, order?: string, userType?:string, searchParam?:string, userStatus?:string):Observable<UsersList>{
    let requestUrl = environment.serverURL.concat("users");
    let params = new HttpParams();
    if(sort)
      params = params.append('sortBy', sort);
    if(order)
      params = params.append('direction', order);
    params = params.append('page', page.toString());
    params = params.append('perPage', perPage.toString());
    if(searchParam)
      params = params.append('searchParam', searchParam);
    if(userStatus)
      params = params.append('status', userStatus);
    if(userType)
      params = params.append('type', userType);
    return this.http.get<UsersList>(requestUrl, {params:params});
  }

  approveUser(id:number):Observable<User>{
    let requestUrl = environment.serverURL.concat(`users/${id}/approve`);
    return this.http.put<User>(requestUrl, {});
  }

  denyUser(id:number):Observable<User>{
    let requestUrl = environment.serverURL.concat(`users/${id}/deny`);
    return this.http.put<User>(requestUrl, {});
  }

  createUser(user:User):Observable<User>{
    let requestUrl = environment.serverURL.concat(`users`);
    return this.http.post<User>(requestUrl, user);
  }

  getById(id:number):Observable<User>{
    let requestUrl = environment.serverURL.concat(`users/${id}`);
    return this.http.get<User>(requestUrl);
  }

  uploadAvatar(file: File, userId:number): Observable<HttpEvent<any>> {
    let requestUrl = environment.serverURL.concat(`users/${userId}/avatar`);
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', requestUrl, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

  getUserAvatar(userId:number, filename:string): Observable<any> {
    let requestUrl = environment.serverURL.concat(`users/${userId}/avatar/${filename}`);
		return this.http.get(requestUrl, {responseType: 'blob'});
  }

  login(credentials:Login):Observable<any>{
    let requestUrl = environment.serverURL.concat(`users/login`);
    return this.http.post<any>(requestUrl, credentials);
  }

  loginExternal(credentials:ExternalAuth):Observable<LoginResponse>{
    let requestUrl = environment.serverURL.concat(`users/external-login`);
    return this.http.post<LoginResponse>(requestUrl, credentials);
  }


}
