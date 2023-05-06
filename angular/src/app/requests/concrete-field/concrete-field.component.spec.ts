import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcreteFieldComponent } from './concrete-field.component';

describe('ConcreteFieldComponent', () => {
  let component: ConcreteFieldComponent;
  let fixture: ComponentFixture<ConcreteFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConcreteFieldComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConcreteFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
