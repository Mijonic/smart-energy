import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkRequestEquipmentComponent } from './work-request-equipment.component';

describe('WorkRequestEquipmentComponent', () => {
  let component: WorkRequestEquipmentComponent;
  let fixture: ComponentFixture<WorkRequestEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkRequestEquipmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkRequestEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
