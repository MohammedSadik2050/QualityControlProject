import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractosListComponent } from './contractos-list.component';

describe('ContractosListComponent', () => {
  let component: ContractosListComponent;
  let fixture: ComponentFixture<ContractosListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContractosListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractosListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
