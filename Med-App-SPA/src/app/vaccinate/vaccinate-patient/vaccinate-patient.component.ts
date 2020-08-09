import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Patient} from "../../_models/patient";
import {UserService} from "../../_service/user.service";
import {AlertifyService} from "../../_service/alertify.service";
import {Vaccines} from "../../_models/vaccines";
import {MedicalService} from "../../_service/medical.service";
import {NgbCalendar, NgbDate} from "@ng-bootstrap/ng-bootstrap";
import {Router} from "@angular/router";

@Component({
  selector: 'app-vaccinate-patient',
  templateUrl: './vaccinate-patient.component.html',
  styleUrls: ['./vaccinate-patient.component.css']
})
export class VaccinatePatientComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  vaccinationRecord: FormGroup;
  patients: Patient[];
  vaccines: Vaccines[];
  dosage: any[] = [
    {"dosage": 0.25},
    {"dosage": 0.5},
    {"dosage": 0.75},
    {"dosage": 1},
  ];
  recordVaccinate: any;
  markDisabled;

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private alertify: AlertifyService,
              private medicalService: MedicalService,
              private calendar: NgbCalendar,
              private router: Router) {
    this.markDisabled = (date: NgbDate) => this.calendar.getWeekday(date) >= 6;

  }

  ngOnInit(): void {
    this.createVaccineRecord();
    this.getPatients();
    this.getVaccines();
  }

  createVaccineRecord() {
    this.vaccinationRecord = this.fb.group({
      patientFKId: [''],
      vacinesFKId: [''],
      timeOfVaccination: [''],
      dosageMl: [''],
      description: ['']
    })
  }

  getPatients() {
    this.userService.getPatients().subscribe((record: Patient[]) => {
      this.patients = record;
    }, error => {
      this.alertify.error(error);
    });
  }

  getVaccines() {
    this.medicalService.getVaccines().subscribe((record: Vaccines[]) => {
      this.vaccines = record;
    }, error => {
      this.alertify.error(error);
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  record() {
    this.recordVaccinate = Object.assign({}, this.vaccinationRecord.value);
    let date = this.vaccinationRecord.controls['timeOfVaccination'].value;
    let format = `${date.year}-${date.month}-${date.day}`;

    this.recordVaccinate['timeOfVaccination'] = format;
    console.log(this.recordVaccinate);
    this.medicalService.createVaccinatedRecord(this.recordVaccinate).subscribe(() => {
      this.alertify.success("Vaccination is recorded to the patient");
      this.router.navigate(['/appointment']);
    }, error => {
      this.alertify.error(error)
    })
  }
}
