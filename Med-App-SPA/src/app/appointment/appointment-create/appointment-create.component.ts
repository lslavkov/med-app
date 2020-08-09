import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MedicalService} from "../../_service/medical.service";
import {AuthService} from "../../_service/auth.service";
import {AlertifyService} from "../../_service/alertify.service";
import {Router} from "@angular/router";
import {NgbCalendar, NgbDate, NgbDatepickerConfig, NgbDateStruct} from "@ng-bootstrap/ng-bootstrap";
import {Physician} from "../../_models/physician";
import {UserService} from "../../_service/user.service";
import {Time} from "@angular/common";

@Component({
  selector: 'app-appointment-create',
  templateUrl: './appointment-create.component.html',
  styleUrls: ['./appointment-create.component.css']
})
export class AppointmentCreateComponent implements OnInit {
  @Output() cancelBooking = new EventEmitter();
  appointmentForm: FormGroup;
  booking: any;
  markDisabled;
  minDate;
  maxDate;
  timePick: Time[] = [
    {hours: 8, minutes: 0},
    {hours: 8, minutes: 15},
    {hours: 8, minutes: 30},
    {hours: 8, minutes: 45},
    {hours: 9, minutes: 0},
    {hours: 9, minutes: 15},
    {hours: 9, minutes: 30},
    {hours: 9, minutes: 45},
    {hours: 10, minutes: 0},
    {hours: 10, minutes: 15},
    {hours: 10, minutes: 30},
    {hours: 10, minutes: 45},
    {hours: 11, minutes: 0},
    {hours: 11, minutes: 15},
    {hours: 11, minutes: 30},
    {hours: 11, minutes: 45},
    {hours: 13, minutes: 0},
    {hours: 13, minutes: 15},
    {hours: 13, minutes: 30},
    {hours: 13, minutes: 45},
    {hours: 14, minutes: 0},
    {hours: 14, minutes: 15},
    {hours: 14, minutes: 30},
    {hours: 14, minutes: 45}
  ];
  typeAppointment: any[] = [
    {"type": "First time"},
    {"type": "Prescription"},
    {"type": "Vaccination"},
    {"type": "Checkup"}
  ];
  physicians: Physician[];

  constructor(private fb: FormBuilder,
              private medicalService: MedicalService,
              private authService: AuthService,
              private alertify: AlertifyService,
              private route: Router,
              private calendar: NgbCalendar,
              private userService: UserService
  ) {
    const current = new Date();
    this.minDate = {
      year: current.getFullYear(),
      month: current.getMonth() + 1,
      day: current.getDate() + 1
    };
    this.maxDate = {
      year: current.getFullYear(),
      month: current.getMonth() + 4,
      day: current.getDate() + 1
    }
    this.markDisabled = (date: NgbDate) => this.calendar.getWeekday(date) >= 6;
    console.log(this.minDate);
  }

  ngOnInit(): void {
    this.createAppointmentForm();
    this.getPhysicians();
  }

  getPhysicians() {
    this.userService.getPhysicians().subscribe((physician: Physician[]) => {
      this.physicians = physician;
    }, error => {
      this.alertify.error(error);
    })
  }

  createAppointmentForm() {
    this.appointmentForm = this.fb.group({
        physicianFKId: ['', Validators.required],
        appointmentPicking: ['', Validators.required],
        timePicker: ['', Validators.required],
        typeOfAppointment: ['', Validators.required],
        description: ['']
      }
    );
  }

  book() {
    if (this.appointmentForm.valid) {
      let date = this.appointmentForm.controls['appointmentPicking'].value;
      let time = this.appointmentForm.controls['timePicker'].value;
      let appointmentFormat = `${date.year}-${date.month}-${date.day} ${time}`;
      console.log(appointmentFormat);
      this.booking = {
        "physicianFKId": this.appointmentForm.controls['physicianFKId'].value,
        "startOfAppointment": appointmentFormat,
        "typeOfAppointment": this.appointmentForm.controls['typeOfAppointment'].value,
        "description": this.appointmentForm.controls['description'].value
      }
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
