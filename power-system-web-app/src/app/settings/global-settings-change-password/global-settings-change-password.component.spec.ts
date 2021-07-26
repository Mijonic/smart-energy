import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSettingsChangePasswordComponent } from './global-settings-change-password.component';

describe('GlobalSettingsChangePasswordComponent', () => {
  let component: GlobalSettingsChangePasswordComponent;
  let fixture: ComponentFixture<GlobalSettingsChangePasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalSettingsChangePasswordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalSettingsChangePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
