import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionTestAssignComponent } from './inspection-test-assign.component';

describe('InspectionTestAssignComponent', () => {
  let component: InspectionTestAssignComponent;
  let fixture: ComponentFixture<InspectionTestAssignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InspectionTestAssignComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionTestAssignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
