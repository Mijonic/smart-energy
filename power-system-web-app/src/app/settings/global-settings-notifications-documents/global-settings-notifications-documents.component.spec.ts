import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSettingsNotificationsDocumentsComponent } from './global-settings-notifications-documents.component';

describe('GlobalSettingsNotificationsDocumentsComponent', () => {
  let component: GlobalSettingsNotificationsDocumentsComponent;
  let fixture: ComponentFixture<GlobalSettingsNotificationsDocumentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalSettingsNotificationsDocumentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalSettingsNotificationsDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
