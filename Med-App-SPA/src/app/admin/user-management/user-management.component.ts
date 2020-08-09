import {Component, OnInit} from '@angular/core';
import {AdminService} from "../../_service/admin.service";
import {User} from "../../_models/user";
import {AlertifyService} from "../../_service/alertify.service";

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: User[];

  constructor(private adminService: AdminService,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      this.alertify.error(error);
    });
  }

  isDisabled(role): boolean {
    return role == 'Admin';
  }

  deleteUser(id: number) {
    this.adminService.deleteUser(id).subscribe(()=>{
      this.alertify.success('User id '+ id + 'is being deleted');
      window.location.reload();
    }, error => {
      this.alertify.error(error);
    })
  }
}
