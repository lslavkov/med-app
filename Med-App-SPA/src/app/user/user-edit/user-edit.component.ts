import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {User} from "../../_models/user";
import {ActivatedRoute} from "@angular/router";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm', { static: true }) editForm: NgForm;
  user: User;

  constructor(private activatedRoute: ActivatedRoute) {
    this.activatedRoute.data.subscribe(data=>{
      this.user = data['user'];
      console.log(data);
      console.log(this.user);
    })
  }

  ngOnInit() {
  }

  updateUser() {

  }

  updateEmail() {

  }
}
