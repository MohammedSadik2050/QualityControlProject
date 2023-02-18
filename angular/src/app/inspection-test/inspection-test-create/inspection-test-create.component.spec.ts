import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionTestCreateComponent } from './inspection-test-create.component';

describe('InspectionTestCreateComponent', () => {
  let component: InspectionTestCreateComponent;
  let fixture: ComponentFixture<InspectionTestCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InspectionTestCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionTestCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
