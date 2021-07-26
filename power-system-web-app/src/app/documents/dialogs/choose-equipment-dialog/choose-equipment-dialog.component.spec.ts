import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseEquipmentDialogComponent } from './choose-equipment-dialog.component';

describe('ChooseEquipmentDialogComponent', () => {
  let component: ChooseEquipmentDialogComponent;
  let fixture: ComponentFixture<ChooseEquipmentDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChooseEquipmentDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseEquipmentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
