import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinesManagementComponent } from './vaccines-management.component';

describe('VaccinesManagementComponent', () => {
  let component: VaccinesManagementComponent;
  let fixture: ComponentFixture<VaccinesManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VaccinesManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VaccinesManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
