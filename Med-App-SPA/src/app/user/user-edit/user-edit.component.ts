import {Component, OnInit} from '@angular/core';
import {User} from "../../_models/user";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../_service/user.service";
import {AlertifyService} from "../../_service/alertify.service";
import {AuthService} from "../../_service/auth.service";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";

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
              private authService: AuthService,
              private modal: NgbModal,
              private route: Router) {

  }

  ngOnInit() {
    this.activatedRoute.data.subscribe(data => {
      this.user = data['users'];
    })
  }

  openModalDelete(content) {
    this.modal.open(content, {backdropClass: 'light-blue-backdrop'})
  }

  deleteAccount() {
    this.userService.deleteUser(this.authService.decodedToken.nameid).subscribe(() => {
      this.route.navigate(['/home']).then(r => localStorage.clear())
      this.alertify.success('This account is being removed')
    })
  }


}
