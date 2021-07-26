
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NewSwitchingInstructionDialogComponent } from 'app/documents/dialogs/new-switching-instruction-dialog/new-switching-instruction-dialog.component';

@Component({
  selector: 'app-work-plan-switching-instructions',
  templateUrl: './work-plan-switching-instructions.component.html',
  styleUrls: ['./work-plan-switching-instructions.component.css']
})
export class WorkPlanSwitchingInstructionsComponent implements OnInit {

  constructor(public dialog:MatDialog) { }

  ngOnInit(): void {
  }

  onAddNew()
  {
    const dialogRef = this.dialog.open(NewSwitchingInstructionDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed and choosen id is ${result}`);
    });
  }

}
