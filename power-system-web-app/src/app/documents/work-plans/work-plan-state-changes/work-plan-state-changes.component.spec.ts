import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkPlanStateChangesComponent } from './work-plan-state-changes.component';

describe('WorkPlanStateChangesComponent', () => {
  let component: WorkPlanStateChangesComponent;
  let fixture: ComponentFixture<WorkPlanStateChangesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkPlanStateChangesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkPlanStateChangesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
