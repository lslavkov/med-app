import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../_models/user";
import {environment} from "../../environments/environment";

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'user/' + id, httpOptions);
  }
}
