import {Routes} from "@angular/router";
import {HomeComponent} from "./app/home/home.component";
import {LoginComponent} from "./app/login/login.component";
import {RegisterComponent} from "./app/register/register.component";

export const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: '',
    children: [
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'register',
        component: RegisterComponent
      }
    ]
  }
];
