import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {AuthService} from "../_service/auth.service";
import {AlertifyService} from "../_service/alertify.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) {
  }

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const roles = next.firstChild.data['roles'] as Array<string>;
    if (roles) {
      const match = this.authService.roleMatch(roles)
      if (match) {
        return true;
      } else {
        this.router.navigate(['/home']);
        this.alertify.error('Error while accessing')
      }
    }
    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('You are not logged in!');
    this.router.navigate(['/home']);
    return false;
  }

}
