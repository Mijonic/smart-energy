import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SafetyDocumentsComponent } from './safety-documents.component';

describe('SafetyDocumentsComponent', () => {
  let component: SafetyDocumentsComponent;
  let fixture: ComponentFixture<SafetyDocumentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SafetyDocumentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SafetyDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
