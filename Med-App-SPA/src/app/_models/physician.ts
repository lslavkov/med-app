import {Patient} from "./patient";
import {Appointments} from "./appointments";
import {Workday} from "./workday";

export interface Physician {
  patients: Patient[]
  appointments: Appointments[]
  workdays: Workday[]
}
