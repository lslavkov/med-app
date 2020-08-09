import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Vaccines} from "../_models/vaccines";
import {User} from "../_models/user";

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

  createVaccine(vaccine:Vaccines){
    return this.http.post(this.baseUrl + 'create/vaccine',vaccine);
  }

  registerPatient(user: User){
    return this.http.post(this.baseUrl + 'register', user);
  }

}
