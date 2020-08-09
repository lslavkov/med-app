import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AdminService} from "../../_service/admin.service";
import {AlertifyService} from "../../_service/alertify.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-physician-registration',
  templateUrl: './physician-registration.component.html',
  styleUrls: ['./physician-registration.component.css']
})
export class PhysicianRegistrationComponent implements OnInit {
  registerForm: FormGroup;
  user: any;
  userName: string;

  constructor(private fb: FormBuilder,
              private adminService: AdminService,
              private alertify: AlertifyService,
              private router : Router) {
  }

  ngOnInit(): void {
    this.createRegisterForm()
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      userName: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      if (this.user.lastName.length < 5) {
        this.userName = this.user.firstName.substring(0, 1) + this.user.lastName.substring(0, 4)
      } else {
        this.userName = this.user.firstName.substring(0, 0) + this.user.lastName.substring(0, 5)
      }
      this.user["userName"] = this.userName
      this.adminService.registerPatient(this.user).subscribe(
        () => {
          this.alertify.success('Successful registration of the new physician');
        }, error => {
          console.log(this.user);
          this.alertify.error(error)
        }, () => {
          this.router.navigate(['/admin'])
        }
      )
    }
  }
}
