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
    ErrorInterceptorProvider,
    UserEditResolver,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
