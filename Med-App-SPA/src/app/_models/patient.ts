import {User} from "./user";
import {Appointments} from "./appointments";

export interface Patient extends User {
  physicianId: number
  appointments: Appointments[]
}
