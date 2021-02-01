import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
import { MessagePopupService } from 'src/app/_services/message-popup.service';
import { UserAddressInfoForCreation } from 'src/app/_models/UserAddressInfoForCreation';

@Component({
  selector: 'app-create-address',
  templateUrl: './create-address.component.html',
  styleUrls: ['./create-address.component.css']
})
export class CreateAddressComponent implements OnInit {
  @Output() creationMode = new EventEmitter();

  model: UserAddressInfoForCreation = <UserAddressInfoForCreation>{};
  decodedToken = this.authService.decodedToken;

  phonePattern = environment.phonePattern;
  nameMaxLength = environment.nameMaxLength;
  surNameMaxLength = environment.surNameMaxLength;
  streetMaxLength = environment.streetMaxLength;
  postCodeMaxLength = environment.postCodeMaxLength;
  cityMaxLength = environment.cityMaxLength;
  countryMaxLength = environment.countryMaxLength;

  constructor(private userService: UserService,
    private authService: AuthService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
  }

  createUserAddress(): void {
    this.userService.createUserAddress(this.decodedToken.nameid, this.model).subscribe(() => {
        this.messagePopup.displaySuccess('User Address was created');
        this.creationMode.emit(false);
    }, error => {
      this.messagePopup.displayError(error);
    });

  }

  cancel() {
    this.creationMode.emit(false);
  }


}
