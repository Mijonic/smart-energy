import { AuthGuardApprovedService } from './auth/auth-guard-approved.service';
import { AuthGuardService } from './auth/auth-guard.service';

import { IncidentDevicesComponent } from './incidents/devices/devices.component';
import { GlobalSettingsStreetsPriorityComponent } from './settings/global-settings-streets-priority/global-settings-streets-priority.component';
import { GlobalSettingsResetDefaultComponent } from './settings/global-settings-reset-default/global-settings-reset-default.component';
import { GlobalSettingsNotificationsDocumentsComponent } from './settings/global-settings-notifications-documents/global-settings-notifications-documents.component';
import { GlobalSettingsIconsComponent } from './settings/global-settings-icons/global-settings-icons.component';
import { GlobalSettingsChangePasswordComponent } from './settings/global-settings-change-password/global-settings-change-password.component';
import { ResolutionComponent } from './incidents/resolution/resolution.component';
import { CallsComponent } from './incidents/calls/calls.component';
import { BasicInformationComponent } from './incidents/basic-information/basic-information.component';
import { WorkPlanSwitchingInstructionsComponent } from './documents/work-plans/work-plan-switching-instructions/work-plan-switching-instructions.component';
import { WorkPlanEquipmentComponent } from './documents/work-plans/work-plan-equipment/work-plan-equipment.component';
import { SafetyDocumentEquipmentComponent } from './documents/safety-documents/safety-document-equipment/safety-document-equipment.component';
import { SafetyDocumentStateChangesComponent } from './documents/safety-documents/safety-document-state-changes/safety-document-state-changes.component';
import { SafetyDocumentBasicInformationComponent } from './documents/safety-documents/safety-document-basic-information/safety-document-basic-information.component';
import { WorkRequestEquipmentComponent } from './documents/work-requests/work-request/work-request-equipment/work-request-equipment.component';
import { WorkRequestStateChangesComponent } from './documents/work-requests/work-request/work-request-state-changes/work-request-state-changes.component';
//import { MultimediaAttachmentsComponent } from './incidents/multimedia-attachments/multimedia-attachments.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { CrewsComponent } from './crews/crews.component';
import { WorkMapComponent } from './map/work-map/work-map.component';
import { InjectionToken, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CrewComponent } from './crews/crew/crew.component';
import { RegistrationComponent } from './front/registration/registration.component';
import { FrontPageComponent } from './front/front-page/front-page.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { WorkPlansComponent } from './documents/work-plans/work-plans.component'; 
import { UsersComponent } from './users/users.component';
import { IncidentsComponent } from './incidents/incidents.component';
import { EditProfileComponent } from './settings/edit-profile/edit-profile.component';
import { IncidentComponent } from './incidents/incident/incident.component';
import { WorkPlanComponent } from './documents/work-plans/work-plan/work-plan.component';
import { SafetyDocumentsComponent } from './documents/safety-documents/safety-documents/safety-documents.component';
import { SafetyDocumentComponent } from './documents/safety-documents/safety-document/safety-document.component';
import { WorkRequestsComponent } from './documents/work-requests/work-requests.component';
import { WorkRequestComponent } from './documents/work-requests/work-request/work-request.component';
import { DevicesComponent } from './devices/devices.component';
import { NewDeviceComponent } from './devices/new-device/new-device.component';
import { GlobalSettingsComponent } from './settings/global-settings/global-settings.component';
import { WorkRequestBasicInformationComponent } from './documents/work-requests/work-request/work-request-basic-information/work-request-basic-information.component';
import { SafetyDocumentChecklistComponent } from './documents/safety-documents/safety-document-checklist/safety-document-checklist.component';
import { IncidentCrewComponent } from './incidents/crew/crew.component';
import { WorkPlanBasicInformationComponent } from './documents/work-plans/work-plan-basic-information/work-plan-basic-information.component';
import { WorkPlanStateChangesComponent } from './documents/work-plans/work-plan-state-changes/work-plan-state-changes.component';
import { IMultimediaService } from './shared/interfaces/multimedia-service';
import { MultimediaAttachmentsComponent } from './multimedia/multimedia-attachments/multimedia-attachments.component';

export const INCIDENT_SERVICE_TOKEN = new InjectionToken<IMultimediaService>("IncidentMultimedia"); 
export const WORK_REQUEST_SERVICE_TOKEN = new InjectionToken<IMultimediaService>("WorkRequestMultimedia");
export const WORK_PLAN_SERVICE_TOKEN = new InjectionToken<IMultimediaService>("WorkPlanMultimedia"); 
export const SAFETY_DOCUMENTS_SERVICE_TOKEN = new InjectionToken<IMultimediaService>("SafetyDocumentMultimedia"); 


const routes: Routes = [
  { path: 'register', component: RegistrationComponent,  outlet:"primary"  },
  { path: '', component: FrontPageComponent,  outlet:"front" },
  { path: 'work-plans', component: WorkPlansComponent,  outlet:"primary" },
  { path: 'work-requests', component: WorkRequestsComponent,
    outlet:"primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
    }
  },
  { path: 'dashboard', component: DashboardComponent,  outlet:"primary", canActivate: [AuthGuardService] },
  { path: 'map/:deviceid', component: WorkMapComponent,  outlet:"primary", canActivate: [AuthGuardService, AuthGuardApprovedService] },
  { path: 'map', component: WorkMapComponent,  outlet:"primary", canActivate: [AuthGuardService, AuthGuardApprovedService] },
  { path: 'crews', component: CrewsComponent,
    outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles:['ADMIN', 'CREW_MEMBER', 'WORKER', 'DISPATCHER']
    }
   },
  { path: 'crew/:id', component: CrewComponent,
   outlet: "primary", 
   canActivate: [AuthGuardService, AuthGuardApprovedService],
   data:{
    roles: ['ADMIN']
   }
   } ,
  { path: 'crew', component: CrewComponent,
    outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['ADMIN']
     }
  } ,
  { path: 'edit-profile', component: EditProfileComponent, outlet: "primary"},
  { path: 'users', component: UsersComponent,
    outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['ADMIN', 'CREW_MEMBER', 'DISPATCHER', 'WORKER']
    }
  },
  { path: 'incidents', component: IncidentsComponent, outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
    }
  },
  { path: 'incident',  redirectTo: '/incident/basic-info', pathMatch: 'full', outlet: "primary"},
  { path: 'incident', component: IncidentComponent, outlet: "primary",
  children:
  [
    {
      path: 'basic-info',
      component: BasicInformationComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['DISPATCHER']
      }
    },
    {
      path: 'basic-info/:id',
      component: BasicInformationComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
    {
      path: 'multimedia/:id',
      component: MultimediaAttachmentsComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        requiredService: INCIDENT_SERVICE_TOKEN,
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
     
    },
    {
      path: 'calls/:id',
      component: CallsComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER' , 'WORKER']
      }
    },
    {
      path: 'crew/:id',
      component: IncidentCrewComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
    {
      path: 'devices/:id',
      component: IncidentDevicesComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
    {
      path: 'resolution/:id',
      component: ResolutionComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
  ],},
  { path: 'work-plan',  redirectTo: '/work-plan/basic-info', pathMatch: 'full', outlet: "primary"},
  { path: 'work-plan', component: WorkPlanComponent, outlet: "primary",
  children:
  [ 
    {
      path: 'basic-info',
      component: WorkPlanBasicInformationComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService]
    },
    {
      path: 'basic-info/:id',
      component: WorkPlanBasicInformationComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService]
    },
    {
      path: 'multimedia',
      component: MultimediaAttachmentsComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService]
    },
    {
      path: 'state-changes',
      component: WorkPlanStateChangesComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService]
    },
    {
      path: 'equipment',
      component: WorkPlanEquipmentComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService]
    },
    {
      path: 'switching-instructions',
      component: WorkPlanSwitchingInstructionsComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService]
    },
  ],
},
  { path: 'safety-documents', component: SafetyDocumentsComponent, outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
    }
  },
  { path: 'safety-document', redirectTo: '/safety-document/basic-info', pathMatch: 'full', outlet: "primary"},
  { path: 'safety-document', component: SafetyDocumentComponent, outlet: "primary",
  children:
  [
    {
      path: 'basic-info',
      component: SafetyDocumentBasicInformationComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['DISPATCHER']
      }
    },

    {
      path: 'basic-info/:id',
      component: SafetyDocumentBasicInformationComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
    {
      path: 'multimedia/:id',
      component: MultimediaAttachmentsComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        requiredService: SAFETY_DOCUMENTS_SERVICE_TOKEN,
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }

     
    },
    {
      path: 'state-changes/:id',
      component: SafetyDocumentStateChangesComponent, 
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
    {
      path: 'equipment/:id',
      component: SafetyDocumentEquipmentComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
    {
      path: 'checklist/:id',
      component: SafetyDocumentChecklistComponent,
      canActivate: [AuthGuardService, AuthGuardApprovedService],
      data:{
        roles: ['CREW_MEMBER', 'DISPATCHER', 'WORKER']
      }
    },
  ],
},
  { path: 'notifications', component: NotificationsComponent, outlet: "primary"},
  { path: 'work-request', redirectTo: '/work-request/basic-info', pathMatch: 'full', outlet: "primary",},
  { path: 'work-request', component: WorkRequestComponent, outlet: "primary",
    children:
    [
      {
        path: 'basic-info',
        component: WorkRequestBasicInformationComponent, 
        canActivate: [AuthGuardService, AuthGuardApprovedService],
        data:{
          roles: ['DISPATCHER']
        }
      },
      {
        path: 'basic-info/:id',
        component: WorkRequestBasicInformationComponent, 
        canActivate: [AuthGuardService, AuthGuardApprovedService],
        data:{
          roles: ['CREW_MEMBER', 'DISPATCHER']
        }
      },
      {
        path: 'multimedia/:id',
        component: MultimediaAttachmentsComponent,
        canActivate: [AuthGuardService, AuthGuardApprovedService],
        data:{
          requiredService: WORK_REQUEST_SERVICE_TOKEN,
          roles: ['CREW_MEMBER', 'DISPATCHER']
        }
      },
      {
        path: 'state-changes/:id',
        component: WorkRequestStateChangesComponent, 
        canActivate: [AuthGuardService, AuthGuardApprovedService],
        data:{
          roles: ['CREW_MEMBER', 'DISPATCHER']
        }
      },
     {
        path: 'equipment/:id',
        component: WorkRequestEquipmentComponent,
        canActivate: [AuthGuardService, AuthGuardApprovedService],
        data:{
          roles: ['CREW_MEMBER', 'DISPATCHER']
        }
      },
    ],
  },
  { path: 'devices', component: DevicesComponent, outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['DISPATCHER', 'WORKER', 'CREW_MEMBER', 'ADMIN']
    }
  },
  { path: 'new-device', component: NewDeviceComponent, outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['ADMIN']
    }
  },
  { path: 'new-device/:id', component: NewDeviceComponent, outlet: "primary",
    canActivate: [AuthGuardService, AuthGuardApprovedService],
    data:{
      roles: ['DISPATCHER', 'WORKER', 'CREW_MEMBER', 'ADMIN']
    }
  },

  { path: 'global-settings', redirectTo: '/global-settings/change-password', pathMatch:"full", outlet: "primary",},
  { path: 'global-settings', component: GlobalSettingsComponent,  outlet: "primary",
  children:
  [
    {
      path: 'change-password',
      component: GlobalSettingsChangePasswordComponent, 
    },
    {
      path: 'icons',
      component: GlobalSettingsIconsComponent, 
    },
    {
      path: 'notifications-documents',
      component: GlobalSettingsNotificationsDocumentsComponent, 
    },
    {
      path: 'reset-default',
      component: GlobalSettingsResetDefaultComponent,
    },
    {
      path: 'streets-priority',
      component: GlobalSettingsStreetsPriorityComponent,
    },
  ],
},
{ path: '**', redirectTo: '', pathMatch:"full", outlet: "primary",}


];

@NgModule({
  imports: [
    RouterModule.forRoot(routes,)],
  exports: [RouterModule]
})
export class AppRoutingModule {


}