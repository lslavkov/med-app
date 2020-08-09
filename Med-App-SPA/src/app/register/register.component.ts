import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {User} from "../_models/user";
import {AuthService} from "../_service/auth.service";
import {Router} from "@angular/router";
import {AlertifyService} from "../_service/alertify.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  user: User;
  userName: any

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.createRegisterForm();
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
      this.authService.register(this.user).subscribe(
        () => {
          this.alertify.success('Registration success');
        }, error => {
          console.log(this.user);
          this.alertify.error(error)
        }, () => {
          this.router.navigate(['/']);
        }
      )
    }
  }


  cancel() {
    this.cancelRegister.emit(false);
  }
}
