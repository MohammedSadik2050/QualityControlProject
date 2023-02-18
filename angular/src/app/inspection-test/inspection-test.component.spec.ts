import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionTestComponent } from './inspection-test.component';

describe('InspectionTestComponent', () => {
  let component: InspectionTestComponent;
  let fixture: ComponentFixture<InspectionTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InspectionTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
