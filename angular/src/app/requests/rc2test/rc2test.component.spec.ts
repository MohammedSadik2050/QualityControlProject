import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Rc2testComponent } from './rc2test.component';

describe('Rc2testComponent', () => {
  let component: Rc2testComponent;
  let fixture: ComponentFixture<Rc2testComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Rc2testComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Rc2testComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
