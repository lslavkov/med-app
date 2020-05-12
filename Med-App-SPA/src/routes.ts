import {Routes} from "@angular/router";
import {HomeComponent} from "./app/home/home.component";
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
        path: 'register',
        component: RegisterComponent
      }
    ]
  }
];
