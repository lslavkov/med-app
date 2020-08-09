import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicianRegistrationComponent } from './physician-registration.component';

describe('PhysicianRegistrationComponent', () => {
  let component: PhysicianRegistrationComponent;
  let fixture: ComponentFixture<PhysicianRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhysicianRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicianRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
