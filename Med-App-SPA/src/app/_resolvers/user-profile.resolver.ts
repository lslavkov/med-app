import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from "@angular/router";
import {User} from "../_models/user";
import {UserService} from "../_service/user.service";
import {AuthService} from "../_service/auth.service";
import {AlertifyService} from "../_service/alertify.service";
import {Observable, of} from "rxjs";
import {catchError} from "rxjs/operators";

@Injectable()
export class UserEditResolver implements Resolve<User> {
  constructor(private userService: UserService, private authService: AuthService, private router: Router, private alertify: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<User>{
    return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
      catchError(errorCatch => {
        this.alertify.error('Problem retreving data');
        this.router.navigate(['/home']);
        return of(null)
      })
    );
  }
}
