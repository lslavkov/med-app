import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinatePatientComponent } from './vaccinate-patient.component';

describe('VaccinatePatientComponent', () => {
  let component: VaccinatePatientComponent;
  let fixture: ComponentFixture<VaccinatePatientComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VaccinatePatientComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VaccinatePatientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
