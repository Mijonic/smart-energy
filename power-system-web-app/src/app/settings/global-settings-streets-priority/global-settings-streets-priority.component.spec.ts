import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSettingsStreetsPriorityComponent } from './global-settings-streets-priority.component';

describe('GlobalSettingsStreetsPriorityComponent', () => {
  let component: GlobalSettingsStreetsPriorityComponent;
  let fixture: ComponentFixture<GlobalSettingsStreetsPriorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalSettingsStreetsPriorityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalSettingsStreetsPriorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
