import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {User} from "../../_models/user";
import {ActivatedRoute} from "@angular/router";
import {FormBuilder, FormGroup, FormGroupDirective, NgForm, Validators} from "@angular/forms";
import {UserService} from "../../_service/user.service";
import {AlertifyService} from "../../_service/alertify.service";
import {AuthService} from "../../_service/auth.service";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  editForm: FormGroup;
  passwordForm: FormGroup;
  user: User;

  constructor(private fb: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private userService: UserService,
              private alertify: AlertifyService,
              private authService: AuthService) {

  }

  createEditPasswordForm() {
    this.passwordForm = this.fb.group(
      {
        oldPassword: ['', Validators.required],
        newPassword: ['', Validators.required]
      }, {validators: this.passwordValidator}
    )
  }

  createEditUserForm() {
    this.editForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required]
    })
  }

  passwordValidator(g: FormGroup) {
    return g.get('oldPassword').value !== g.get('newPassword').value ? null : {mismatch: true};
  }

  ngOnInit() {
    this.activatedRoute.data.subscribe(data => {
      this.user = data['users'];
      console.log(this.user);
    })
    this.createEditPasswordForm();
    this.createEditUserForm();
  }

  updateUser() {
    if (this.editForm.valid) {
      this.user = Object.assign({}, this.editForm.value);
      this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(
        next => {
          this.alertify.success('Profile has being updated');
          this.editForm.reset()
          console.log(this.user);
        }, error => {
          this.alertify.error(error);
        }
      );
    }
  }

  updatePassword() {
    if (this.passwordForm.valid) {
      this.user = Object.assign({}, this.passwordForm.value);
      this.userService.changePassword(this.user).subscribe(() => {
        this.alertify.success('Password was changed successfully');
        this.passwordForm.reset();
      }, error => {
        this.alertify.error(error)
      });
    }
  }
}
