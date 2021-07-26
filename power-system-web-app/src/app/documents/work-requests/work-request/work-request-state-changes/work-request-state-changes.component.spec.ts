import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkRequestStateChangesComponent } from './work-request-state-changes.component';

describe('WorkRequestStateChangesComponent', () => {
  let component: WorkRequestStateChangesComponent;
  let fixture: ComponentFixture<WorkRequestStateChangesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkRequestStateChangesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkRequestStateChangesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
