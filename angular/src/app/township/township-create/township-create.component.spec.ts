import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TownshipCreateComponent } from './township-create.component';

describe('TownshipCreateComponent', () => {
  let component: TownshipCreateComponent;
  let fixture: ComponentFixture<TownshipCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TownshipCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TownshipCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
