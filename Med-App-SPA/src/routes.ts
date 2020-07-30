import {Routes} from "@angular/router";
import {HomeComponent} from "./app/home/home.component";
import {AuthGuard} from "./app/_guards/auth.guard";
import {UserEditResolver} from "./app/_resolvers/user-profile.resolver";
import {UserEditComponent} from "./app/user/user-edit/user-edit.component";
import {ConfirmComponent} from "./app/confirm/confirm.component";
import {RegisterComponent} from "./app/register/register.component";
import {AppointmentListComponent} from "./app/appointment/appointment-list/appointment-list.component";
import {AppointmentCreateComponent} from "./app/appointment/appointment-create/appointment-create.component";

export const appRoutes: Routes = [
  {path: 'home', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'appointment', component: AppointmentListComponent, data: {roles: ['Patient', 'Physician']}},
      {path: 'user/edit', component: UserEditComponent, resolve: {users: UserEditResolver}},
      {path: 'appointment/create', component: AppointmentCreateComponent}
    ]
  },
  {path: 'confirm', component: ConfirmComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', redirectTo: 'home', pathMatch: 'full'}

];
