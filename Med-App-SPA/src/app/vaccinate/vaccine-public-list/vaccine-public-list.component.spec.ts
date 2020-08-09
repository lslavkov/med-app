import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinePublicListComponent } from './vaccine-public-list.component';

describe('VaccinePublicListComponent', () => {
  let component: VaccinePublicListComponent;
  let fixture: ComponentFixture<VaccinePublicListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VaccinePublicListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VaccinePublicListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
