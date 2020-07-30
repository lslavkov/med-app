import {Component, OnInit} from '@angular/core';
import {MedicalService} from "../../_service/medical.service";
import {Appointments} from "../../_models/appointments";
import {AlertifyService} from "../../_service/alertify.service";

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {
  currentAppointment: Appointments[]

  constructor(private medicalService: MedicalService, private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.loadUpcomingAppointments()
  }

  loadUpcomingAppointments() {
    this.medicalService.getUpcomingAppointments().subscribe((appointments: Appointments[]) => {
      this.currentAppointment = appointments;
      console.log(this.currentAppointment.length);
    }, error => {
      this.alertify.error(error);
    })
  }
}
