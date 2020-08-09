import {Component, OnInit} from '@angular/core';
import {Appointments} from "../../_models/appointments";
import {AdminService} from "../../_service/admin.service";
import {AlertifyService} from "../../_service/alertify.service";

@Component({
  selector: 'app-appointment-management',
  templateUrl: './appointment-management.component.html',
  styleUrls: ['./appointment-management.component.css']
})
export class AppointmentManagementComponent implements OnInit {
  appointments: Appointments[]

  constructor(private adminService: AdminService, private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.getAppointments();
  }

  getAppointments() {
    this.adminService.getAppointments().subscribe((appointments: Appointments[]) => {
      this.appointments = appointments;
    }, error => {
      this.alertify.error(error);
    })
  }
}
