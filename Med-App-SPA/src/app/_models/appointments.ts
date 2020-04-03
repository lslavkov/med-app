import {Time} from "@angular/common";

export interface Appointments {
  id:number
  patientId:number
  physicianId:number
  timeOfAppointment:Time
  typeOfAppointment:string
}
