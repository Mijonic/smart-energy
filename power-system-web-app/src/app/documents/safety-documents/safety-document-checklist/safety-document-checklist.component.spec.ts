import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SafetyDocumentChecklistComponent } from './safety-document-checklist.component';

describe('SafetyDocumentChecklistComponent', () => {
  let component: SafetyDocumentChecklistComponent;
  let fixture: ComponentFixture<SafetyDocumentChecklistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SafetyDocumentChecklistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SafetyDocumentChecklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
