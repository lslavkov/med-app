import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicianWorkComponent } from './physician-work.component';

describe('PhysicianWorkComponent', () => {
  let component: PhysicianWorkComponent;
  let fixture: ComponentFixture<PhysicianWorkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhysicianWorkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicianWorkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
