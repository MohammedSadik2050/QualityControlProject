import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectRejectModalComponent } from './project-reject-modal.component';

describe('ProjectRejectModalComponent', () => {
  let component: ProjectRejectModalComponent;
  let fixture: ComponentFixture<ProjectRejectModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectRejectModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectRejectModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
