import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TownshipEditComponent } from './township-edit.component';

describe('TownshipEditComponent', () => {
  let component: TownshipEditComponent;
  let fixture: ComponentFixture<TownshipEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TownshipEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TownshipEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
