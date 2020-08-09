import {Component, OnInit} from '@angular/core';
import {MedicalService} from "../../_service/medical.service";
import {PatientVaccinateds} from "../../_models/patientVaccinateds";
import {AlertifyService} from "../../_service/alertify.service";
import {Vaccines} from "../../_models/vaccines";

@Component({
  selector: 'app-vaccinate-list',
  templateUrl: './vaccinate-list.component.html',
  styleUrls: ['./vaccinate-list.component.css']
})
export class VaccinateListComponent implements OnInit {
  vaccineRecord: PatientVaccinateds[]


  constructor(private medicalService: MedicalService,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.getPatientsVaccineRecord()
  }

  getPatientsVaccineRecord() {
    this.medicalService.getPatientsVaccines().subscribe((record: PatientVaccinateds[]) => {
      this.vaccineRecord = record;
      console.log(this.vaccineRecord);
    }, error => {
      this.alertify.error(error);
    })
  }
}
