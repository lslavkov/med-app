import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../_models/user";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'user/' + id);
  }

  changePassword(user): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'user/change/password', user);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + 'user/change/' + id, user)
  }
}
