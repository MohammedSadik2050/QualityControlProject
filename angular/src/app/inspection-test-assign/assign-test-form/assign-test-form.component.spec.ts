import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignTestFormComponent } from './assign-test-form.component';

describe('AssignTestFormComponent', () => {
  let component: AssignTestFormComponent;
  let fixture: ComponentFixture<AssignTestFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignTestFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignTestFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
