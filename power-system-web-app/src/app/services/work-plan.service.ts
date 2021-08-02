import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WorkPlan } from 'app/shared/models/work-plan.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class WorkPlanService {

  constructor(private http: HttpClient) { }
  
  getAllWorkPlans():Observable<WorkPlan[]>{
    let requestUrl = environment.serverURL.concat("work-plan");
    return this.http.get<WorkPlan[]>(requestUrl);
  }
}
