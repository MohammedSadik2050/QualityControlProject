import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractorUserComponent } from './contractor-user.component';

describe('ContractorUserComponent', () => {
  let component: ContractorUserComponent;
  let fixture: ComponentFixture<ContractorUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContractorUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractorUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
