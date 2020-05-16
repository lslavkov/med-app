import {Component, OnInit} from '@angular/core';
import {User} from "../../_models/user";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  user: User

  constructor(private activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.activatedRoute.data.subscribe(data => {
      this.user = data['user'];
    });
  }

}
