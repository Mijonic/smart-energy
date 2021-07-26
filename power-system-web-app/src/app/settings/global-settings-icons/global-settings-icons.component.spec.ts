import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSettingsIconsComponent } from './global-settings-icons.component';

describe('GlobalSettingsIconsComponent', () => {
  let component: GlobalSettingsIconsComponent;
  let fixture: ComponentFixture<GlobalSettingsIconsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalSettingsIconsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalSettingsIconsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
