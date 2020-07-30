import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MedicalService} from "../../_service/medical.service";
import {AuthService} from "../../_service/auth.service";
import {AlertifyService} from "../../_service/alertify.service";
import {Router} from "@angular/router";
import {DatePipe, Time} from "@angular/common";

@Component({
  selector: 'app-appointment-create',
  templateUrl: './appointment-create.component.html',
  styleUrls: ['./appointment-create.component.css']
})
export class AppointmentCreateComponent implements OnInit {
  @Output() cancelBooking = new EventEmitter();
  appointmentForm: FormGroup;
  booking: any;

  constructor(private fb: FormBuilder,
              private medicalService: MedicalService,
              private authService: AuthService,
              private alertify: AlertifyService,
              private route: Router,
              private datePipe: DatePipe) {
  }

  ngOnInit(): void {
    this.createAppointmentForm();
  }

  createAppointmentForm() {
    this.appointmentForm = this.fb.group({
        physicianFKId: ['', Validators.required],
        startOfAppointment: ['', Validators.required],
        description: ['']
      }
    );
  }

  book() {
    if (this.appointmentForm.valid) {
      // this.booking.dateBooking = this.datePipe.transform(this.booking.dataLevel, 'yyyy-MM-dd HH:mm');

      this.booking = Object.assign({}, this.appointmentForm.value);
      // this.booking.dateBooking = new Date(this.booking.dateBooking);
      // this.booking.dateBooking = this.datePipe.transform(this.booking.dataLevel, 'yyyy-MM-dd HH:mm');
      console.log(this.booking);
      this.medicalService.createAppointment(this.authService.decodedToken.nameid, this.booking).subscribe(() => {
        this.alertify.success('Booking successful');
        this.route.navigate(['/appointment']);
      }, error => {
        this.alertify.error(error);
      })
    }
  }

  cancel() {
    this.cancelBooking.emit(false);
  }
}
