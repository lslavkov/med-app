import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
// import {JwtHelperService} from '@auth0/angular-jwt'
import {User} from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  // jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {
  }

  register(user: User){
    return this.http.post(this.baseUrl + 'register', user);
  }

}
