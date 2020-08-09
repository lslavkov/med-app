import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEditProfilComponent } from './user-edit-profil.component';

describe('UserEditProfilComponent', () => {
  let component: UserEditProfilComponent;
  let fixture: ComponentFixture<UserEditProfilComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserEditProfilComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserEditProfilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
