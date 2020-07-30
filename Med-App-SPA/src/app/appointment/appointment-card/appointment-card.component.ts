import {Component, Input, OnInit} from '@angular/core';
import {Appointments} from "../../_models/appointments";
import {MedicalService} from "../../_service/medical.service";
import {AlertifyService} from "../../_service/alertify.service";

@Component({
  selector: 'app-appointment-card',
  templateUrl: './appointment-card.component.html',
  styleUrls: ['./appointment-card.component.css']
})
export class AppointmentCardComponent implements OnInit {
  @Input() appointment: Appointments;

  constructor(private medicalService: MedicalService, private alertify: AlertifyService) {
  }

  ngOnInit(): void {
  }

  deleteAppointment(id: number) {
    this.medicalService.deleteAppointment(id).subscribe(() => {
      this.alertify.success('Appointment is deleted');
      this.reload();
    }, error => {
      this.alertify.error(error);
    })
  }

  reload() {
    window.location.reload();
  }
}
