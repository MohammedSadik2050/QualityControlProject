import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionTestEditComponent } from './inspection-test-edit.component';

describe('InspectionTestEditComponent', () => {
  let component: InspectionTestEditComponent;
  let fixture: ComponentFixture<InspectionTestEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InspectionTestEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionTestEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
