import {Component, OnInit} from '@angular/core';
import {Vaccines} from "../../_models/vaccines";
import {MedicalService} from "../../_service/medical.service";
import {AlertifyService} from "../../_service/alertify.service";

@Component({
  selector: 'app-vaccine-public-list',
  templateUrl: './vaccine-public-list.component.html',
  styleUrls: ['./vaccine-public-list.component.css']
})
export class VaccinePublicListComponent implements OnInit {
  vaccineList: Vaccines[];

  constructor(private medicalService: MedicalService,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.getVaccineList();
  }

  getVaccineList() {
    this.medicalService.getVaccines().subscribe((record: Vaccines[]) => {
      this.vaccineList = record;
    }, error => {
      this.alertify.error(error);
    })
  }
}
