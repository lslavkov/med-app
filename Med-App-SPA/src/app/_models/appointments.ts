import {Time} from "@angular/common";

export interface Appointments {
  id: number
  patientId: number
  patientFullName:string
  physicianId: number
  physicianFullName:string
  startOfAppointment: Time
  endOfAppointment: Time
  description?: string
}
