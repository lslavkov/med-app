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
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserProfileComponent } from './user/user-profile/user-profile.component';
import {UserProfileResolver} from "./_resolvers/user-profile.resolver";

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
    UserProfileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    ReactiveFormsModule,
    HttpClientModule,
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
    UserProfileResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
