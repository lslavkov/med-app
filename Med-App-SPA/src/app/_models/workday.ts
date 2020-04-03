import {Time} from "@angular/common";

export interface Workday {
  id:number
  physicianId:number
  start:Time
  finish:Time
}
