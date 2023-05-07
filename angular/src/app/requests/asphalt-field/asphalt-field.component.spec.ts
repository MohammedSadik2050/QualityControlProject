import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsphaltFieldComponent } from './asphalt-field.component';

describe('AsphaltFieldComponent', () => {
  let component: AsphaltFieldComponent;
  let fixture: ComponentFixture<AsphaltFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsphaltFieldComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AsphaltFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
