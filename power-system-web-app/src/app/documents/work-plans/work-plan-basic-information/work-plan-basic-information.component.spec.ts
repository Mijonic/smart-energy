import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkPlanBasicInformationComponent } from './work-plan-basic-information.component';

describe('WorkPlanBasicInformationComponent', () => {
  let component: WorkPlanBasicInformationComponent;
  let fixture: ComponentFixture<WorkPlanBasicInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkPlanBasicInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkPlanBasicInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
