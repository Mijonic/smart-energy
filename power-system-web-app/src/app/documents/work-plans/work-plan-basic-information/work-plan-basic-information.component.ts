
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChooseIncidentDialogComponent } from 'app/documents/dialogs/choose-incident-dialog/choose-incident-dialog.component';
import { ChooseWorkRequestDialogComponent } from 'app/documents/dialogs/choose-work-request-dialog/choose-work-request-dialog.component';

@Component({
  selector: 'app-work-plan-basic-information',
  templateUrl: './work-plan-basic-information.component.html',
  styleUrls: ['./work-plan-basic-information.component.css']
})
export class WorkPlanBasicInformationComponent implements OnInit {
  documentTypes:string[] = ['Planned work', 'Unplanned work'];

  constructor(public dialog:MatDialog) { }

  ngOnInit(): void {
  }

  onChooseIncident()
  {
    const dialogRef = this.dialog.open(ChooseIncidentDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed and choosen id is ${result}`);
    });
  }
 
  

  onChooseWorkRequest()
  {
    const dialogRef = this.dialog.open(ChooseWorkRequestDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed and choosen id is ${result}`);
    });

  }

}

