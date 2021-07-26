import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseWorkRequestDialogComponent } from './choose-work-request-dialog.component';

describe('ChooseWorkRequestDialogComponent', () => {
  let component: ChooseWorkRequestDialogComponent;
  let fixture: ComponentFixture<ChooseWorkRequestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChooseWorkRequestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseWorkRequestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
