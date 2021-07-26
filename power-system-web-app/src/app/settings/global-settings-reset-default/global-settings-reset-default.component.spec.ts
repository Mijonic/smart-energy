import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSettingsResetDefaultComponent } from './global-settings-reset-default.component';

describe('GlobalSettingsResetDefaultComponent', () => {
  let component: GlobalSettingsResetDefaultComponent;
  let fixture: ComponentFixture<GlobalSettingsResetDefaultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalSettingsResetDefaultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalSettingsResetDefaultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
