import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkRequestBasicInformationComponent } from './work-request-basic-information.component';

describe('WorkRequestBasicInformationComponent', () => {
  let component: WorkRequestBasicInformationComponent;
  let fixture: ComponentFixture<WorkRequestBasicInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkRequestBasicInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkRequestBasicInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
