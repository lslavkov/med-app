import {Component, OnInit} from '@angular/core';
import {AuthService} from "../_service/auth.service";
import {AlertifyService} from "../_service/alertify.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) {
  }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('You are logged in!');
      }, error => {
        this.alertify.error(error);
      }
    );
  }

}
