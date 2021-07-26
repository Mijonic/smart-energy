import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkPlanComponent } from './work-plan.component';

describe('WorkPlanComponent', () => {
  let component: WorkPlanComponent;
  let fixture: ComponentFixture<WorkPlanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkPlanComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
