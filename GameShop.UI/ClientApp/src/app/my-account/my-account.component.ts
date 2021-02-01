import { MessagePopupService } from './../_services/message-popup.service';
import { UserForAccInfoFromServer } from './../_models/UserForAccInfoFromServer';
import { AuthService } from './../_services/auth.service';
import { UserService } from './../_services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css']
})
export class MyAccountComponent implements OnInit {
  model: UserForAccInfoFromServer = <UserForAccInfoFromServer>{};
  decodedToken = this.authService.decodedToken;

  constructor(private userService: UserService, private authService: AuthService, private messagePopupService: MessagePopupService) { }

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

  cancel() {

  }

}
