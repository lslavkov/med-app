import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
// import {JwtHelperService} from '@auth0/angular-jwt'
import {Patient} from "../_models/patient";
import {Physician} from "../_models/physician";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http:localhost:5000/api/auth/';
  // jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentPatient: Patient;
  currentPhysician: Physician;

  constructor(private http: HttpClient) {
  }

  registerPatient(patient: Patient) {
    return this.http.post(this.baseUrl + 'register/patient', patient);
  }
  registerPhysician(physician: Physician) {
    return this.http.post(this.baseUrl + 'register/physician', physician);
  }
}
