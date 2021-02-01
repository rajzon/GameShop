import { AuthService } from './../../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { UserForAccInfoFromServer } from 'src/app/_models/UserForAccInfoFromServer';
import { UserService } from 'src/app/_services/user.service';
import { MessagePopupService } from 'src/app/_services/message-popup.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-change-user-info',
  templateUrl: './change-user-info.component.html',
  styleUrls: ['./change-user-info.component.css']
})
export class ChangeUserInfoComponent implements OnInit {
  model: UserForAccInfoFromServer = <UserForAccInfoFromServer>{};
  decodedToken = this.authService.decodedToken;

  phonePattern = environment.phonePattern;

  nameMaxLength = environment.nameMaxLength;
  surNameMaxLength = environment.surNameMaxLength;

  constructor(private userService: UserService, private authService: AuthService,
            private messagePopupService: MessagePopupService, private router: Router) { }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    console.log(this.decodedToken);
    this.userService.getUserForAccInfo(<number> this.decodedToken.nameid).subscribe((response: UserForAccInfoFromServer) => {
        this.model = response;
    }, error => {
      this.messagePopupService.displayError(error);
    });
  }

  editUser() {
    this.userService.editUserAccInfo(this.decodedToken.nameid, this.model).subscribe(() => {
      this.messagePopupService.displaySuccess('User account information has been edited');
      this.router.navigateByUrl('/account');
    }, error => {
      this.messagePopupService.displayError(error);
    });
  }

  cancel() {
    this.router.navigateByUrl('/account');
  }

}
