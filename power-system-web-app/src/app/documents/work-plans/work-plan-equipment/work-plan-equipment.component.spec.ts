import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkPlanEquipmentComponent } from './work-plan-equipment.component';

describe('WorkPlanEquipmentComponent', () => {
  let component: WorkPlanEquipmentComponent;
  let fixture: ComponentFixture<WorkPlanEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkPlanEquipmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkPlanEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
