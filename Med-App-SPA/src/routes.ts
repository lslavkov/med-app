import {Routes} from "@angular/router";
import {HomeComponent} from "./app/home/home.component";
import {AuthGuard} from "./app/_guards/auth.guard";
import {UserEditResolver} from "./app/_resolvers/user-profile.resolver";
import {UserEditComponent} from "./app/user/user-edit/user-edit.component";
import {ConfirmComponent} from "./app/confirm/confirm.component";
import {RegisterComponent} from "./app/register/register.component";
import {AppointmentListComponent} from "./app/appointment/appointment-list/appointment-list.component";
import {AppointmentCreateComponent} from "./app/appointment/appointment-create/appointment-create.component";
import {AdminPanelComponent} from "./app/admin/admin-panel/admin-panel.component";
import {VaccinateListComponent} from "./app/vaccinate/vaccinate-list/vaccinate-list.component";
import {VaccinatePatientComponent} from "./app/vaccinate/vaccinate-patient/vaccinate-patient.component";
import {VaccinePublicListComponent} from "./app/vaccinate/vaccine-public-list/vaccine-public-list.component";

export const appRoutes: Routes = [
  {path: 'home', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'appointment', component: AppointmentListComponent, data: {roles: ['Patient', 'Physician']}},
      {path: 'user/edit', component: UserEditComponent, resolve: {users: UserEditResolver}},
      {path: 'create/appointment', component: AppointmentCreateComponent},
      {path: 'vaccines', component: VaccinateListComponent},
      {path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin']}},
      {path: 'create/vaccinationRecord', component: VaccinatePatientComponent},
      {path: 'vaccineList', component: VaccinePublicListComponent}
    ]
  },
  {path: 'confirm', component: ConfirmComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', redirectTo: 'home', pathMatch: 'full'}

];
