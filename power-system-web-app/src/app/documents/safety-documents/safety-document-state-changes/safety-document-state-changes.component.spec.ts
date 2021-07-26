import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SafetyDocumentStateChangesComponent } from './safety-document-state-changes.component';

describe('SafetyDocumentStateChangesComponent', () => {
  let component: SafetyDocumentStateChangesComponent;
  let fixture: ComponentFixture<SafetyDocumentStateChangesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SafetyDocumentStateChangesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SafetyDocumentStateChangesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
