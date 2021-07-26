import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsMultimediaAttachmentComponent } from './documents-multimedia-attachment.component';

describe('DocumentsMultimediaAttachmentComponent', () => {
  let component: DocumentsMultimediaAttachmentComponent;
  let fixture: ComponentFixture<DocumentsMultimediaAttachmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentsMultimediaAttachmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentsMultimediaAttachmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
