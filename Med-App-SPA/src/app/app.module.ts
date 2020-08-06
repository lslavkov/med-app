import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

import {AppComponent} from './app.component';
import {NavComponent} from './nav/nav.component';
import {RegisterComponent} from './register/register.component';
import {HomeComponent} from './home/home.component';
import {RouterModule} from "@angular/router";
import {appRoutes} from "../routes";
import {FooterComponent} from './footer/footer.component';
import {JwtModule} from "@auth0/angular-jwt";
import {ErrorInterceptorProvider} from "./_service/error.interceptor";
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {UserEditComponent} from "./user/user-edit/user-edit.component";
import {UserEditResolver} from "./_resolvers/user-profile.resolver";
import {ConfirmComponent} from './confirm/confirm.component';
import {AppointmentListComponent} from './appointment/appointment-list/appointment-list.component';
import {AppointmentCardComponent} from './appointment/appointment-card/appointment-card.component';
import {HasRoleDirective} from './_directives/has-role.directive';
import { AppointmentCreateComponent } from './appointment/appointment-create/appointment-create.component';
import {DatePipe} from "@angular/common";
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { AppointmentManagementComponent } from './admin/appointment-management/appointment-management.component';
import {AuthService} from "./_service/auth.service";
import {AlertifyService} from "./_service/alertify.service";
import {UserService} from "./_service/user.service";
import {AdminService} from "./_service/admin.service";
import {MedicalService} from "./_service/medical.service";

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    HomeComponent,
    FooterComponent,
    UserEditComponent,
    ConfirmComponent,
    AppointmentListComponent,
    AppointmentCardComponent,
    HasRoleDirective,
    AppointmentCreateComponent,
    AdminPanelComponent,
    UserManagementComponent,
    AppointmentManagementComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
    NgbModule
  ],
  providers: [
    AuthService,
    AlertifyService,
    UserService,
    AdminService,
    MedicalService,
    ErrorInterceptorProvider,
    UserEditResolver,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
