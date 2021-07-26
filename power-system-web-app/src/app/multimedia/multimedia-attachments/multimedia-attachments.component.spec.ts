import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultimediaAttachmentsComponent } from './multimedia-attachments.component';

describe('MultimediaAttachmentsComponent', () => {
  let component: MultimediaAttachmentsComponent;
  let fixture: ComponentFixture<MultimediaAttachmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultimediaAttachmentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MultimediaAttachmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
