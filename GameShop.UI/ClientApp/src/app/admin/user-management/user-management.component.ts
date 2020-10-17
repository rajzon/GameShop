import { MessagePopupService } from './../../_services/message-popup.service';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: User[];
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles(): void {
    this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  editRolesModal(user: User): void {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
          
          
        }, error => {
          this.messagePopup.displayError(error);
        });
      }
    });
  }

  private getRolesArray(user: User): any[] {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = environment.availableRoles;

    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }

}
