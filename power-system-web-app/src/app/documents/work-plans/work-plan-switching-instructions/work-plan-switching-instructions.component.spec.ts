import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkPlanSwitchingInstructionsComponent } from './work-plan-switching-instructions.component';

describe('WorkPlanSwitchingInstructionsComponent', () => {
  let component: WorkPlanSwitchingInstructionsComponent;
  let fixture: ComponentFixture<WorkPlanSwitchingInstructionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkPlanSwitchingInstructionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkPlanSwitchingInstructionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
