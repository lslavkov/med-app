import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl + 'admin/';

  constructor(private http: HttpClient) {
  }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'usersWithRoles');
  }

  getAppointments() {
    return this.http.get(this.baseUrl + 'getAppointments');
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'delete/user/' + id);
  }
}
