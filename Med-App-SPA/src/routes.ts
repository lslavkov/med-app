import {Routes} from "@angular/router";
import {HomeComponent} from "./app/home/home.component";
import {RegisterComponent} from "./app/register/register.component";
import {UserProfileComponent} from "./app/user/user-profile/user-profile.component";
import {UserProfileResolver} from "./app/_resolvers/user-profile.resolver";

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
      },
      {
        path: 'editUser',
        component: UserProfileComponent,
        resolve: {user: UserProfileResolver}
      }
    ]
  }
];
