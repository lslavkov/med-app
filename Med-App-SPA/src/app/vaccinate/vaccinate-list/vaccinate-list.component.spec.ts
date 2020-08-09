import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinateListComponent } from './vaccinate-list.component';

describe('VaccinateListComponent', () => {
  let component: VaccinateListComponent;
  let fixture: ComponentFixture<VaccinateListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VaccinateListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VaccinateListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
