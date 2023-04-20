import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObserverEditComponent } from './observer-edit.component';

describe('ObserverEditComponent', () => {
  let component: ObserverEditComponent;
  let fixture: ComponentFixture<ObserverEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObserverEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObserverEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
