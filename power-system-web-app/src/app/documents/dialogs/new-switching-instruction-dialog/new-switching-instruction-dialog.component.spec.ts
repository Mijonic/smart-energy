import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewSwitchingInstructionDialogComponent } from './new-switching-instruction-dialog.component';

describe('NewSwitchingInstructionDialogComponent', () => {
  let component: NewSwitchingInstructionDialogComponent;
  let fixture: ComponentFixture<NewSwitchingInstructionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewSwitchingInstructionDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewSwitchingInstructionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
