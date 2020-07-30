import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Appointments} from "../_models/appointments";

@Injectable({
  providedIn: 'root'
})
export class MedicalService {
  baseUrl = environment.apiUrl + 'medical/';

  constructor(private http: HttpClient) {
  }

  getUpcomingAppointments() {
    return this.http.get(this.baseUrl + 'get/upcoming/appointments')
  }

  deleteAppointment(id: number) {
    return this.http.delete(this.baseUrl + 'delete/appointment/' + id);
  }

  createAppointment(id: number, appointment: Appointments) {
    return this.http.post(this.baseUrl + 'create/user/' + id + '/appointment', appointment)
  }

}
