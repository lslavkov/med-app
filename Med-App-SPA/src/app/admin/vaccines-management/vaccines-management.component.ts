import {Component, OnInit} from '@angular/core';
import {Vaccines} from "../../_models/vaccines";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MedicalService} from "../../_service/medical.service";
import {AdminService} from "../../_service/admin.service";
import {AlertifyService} from "../../_service/alertify.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-vaccines-management',
  templateUrl: './vaccines-management.component.html',
  styleUrls: ['./vaccines-management.component.css']
})
export class VaccinesManagementComponent implements OnInit {
  vaccines: Vaccines[];
  vaccineForm: FormGroup;
  addVaccineForm: Vaccines;

  constructor(private modal: NgbModal,
              private fb: FormBuilder,
              private medicalService: MedicalService,
              private adminService: AdminService,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.addVaccine()
    this.getVaccines();
  }

  addVaccine() {
    this.vaccineForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  addNewVaccine() {
    if (this.vaccineForm.valid) {
      this.addVaccineForm = Object.assign({}, this.vaccineForm.value);
      this.adminService.createVaccine(this.addVaccineForm).subscribe(() => {
        this.alertify.success('Added new vaccine');
        window.location.reload();
      },error => {
        this.alertify.error(error);
      });
    }
  }

  getVaccines() {
    this.medicalService.getVaccines().subscribe((record: Vaccines[]) => {
      this.vaccines = record;
    })
  }

  openModalDelete(content) {
    this.modal.open(content, {backdropClass: 'light-blue-backdrop'})
  }

}
