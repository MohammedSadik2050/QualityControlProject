import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObserverCreateComponent } from './observer-create.component';

describe('ObserverCreateComponent', () => {
  let component: ObserverCreateComponent;
  let fixture: ComponentFixture<ObserverCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObserverCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObserverCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
