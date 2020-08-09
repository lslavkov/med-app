import {Time} from "@angular/common";

export interface Appointments {
  typeOfAppointment: string;
  id: number
  patientId: number
  patientFullName:string
  physicianId: number
  physicianFullName:string
  startOfAppointment: Time
  endOfAppointment: Time
  description?: string
}
