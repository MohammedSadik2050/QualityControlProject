import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgenyComponent } from './ageny.component';

describe('AgenyComponent', () => {
  let component: AgenyComponent;
  let fixture: ComponentFixture<AgenyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgenyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgenyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
