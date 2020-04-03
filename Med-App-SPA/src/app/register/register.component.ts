import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {User} from "../_models/user";
import {AuthService} from "../_service/auth.service";
import {Physician} from "../_models/physician";
import {Patient} from "../_models/patient";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  user: User;
  patient: Patient;
  physician: Physician;

  constructor(private fb: FormBuilder, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
        type: ['patient'],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(8)
          ]
        ],
        confirmPassword: ['', Validators.required],
      }, {validators: this.passwordMatchValidator}
    );
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value
      ? null
      : {mismatch: true};
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      if (this.user['type'] === 'patient') {
        this.patient = Object.assign({}, this.registerForm.value);
        this.authService.registerPatient(this.patient).subscribe(() => {
          console.log('Registration succesfull as patient')
        }, error => {
          console.log(error);
        })
      } else if (this.user['type'] === 'physician') {
        this.physician = Object.assign({}, this.registerForm.value);
        this.authService.registerPhysician(this.physician).subscribe(() => {
          console.log('Registration succesfull as physician')
        }, error => {
          console.log(error);
        })
      }
    }
  }


  cancel() {
    this.cancelRegister.emit(false);
  }
}
