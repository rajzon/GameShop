import { UserAddressFromServer } from './../../_models/UserAddressFromServer';
import { MessagePopupService } from './../../_services/message-popup.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from './../../_services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserAddressInfoForEdition } from './../../_models/UserAddressInfoForEdition';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-edit-address',
  templateUrl: './edit-address.component.html',
  styleUrls: ['./edit-address.component.css']
})
export class EditAddressComponent implements OnInit {
  @Input() userAddressToEdit: UserAddressFromServer;
  @Output() editionMode = new EventEmitter();
  editAddressForm: FormGroup;

  phonePattern = environment.phonePattern;
  nameMaxLength = environment.nameMaxLength;
  surNameMaxLength = environment.surNameMaxLength;
  streetMaxLength = environment.streetMaxLength;
  postCodeMaxLength = environment.postCodeMaxLength;
  cityMaxLength = environment.cityMaxLength;
  countryMaxLength = environment.countryMaxLength;
  emailPattern = environment.emailPattern;

  constructor(private userService: UserService, private authService: AuthService,
          private messagePopup: MessagePopupService) { }

  ngOnInit() {
    console.log(this.userAddressToEdit);
    this.initForm();
  }

  initForm(): void {
    this.editAddressForm = new FormGroup({
      name: new FormControl(this.userAddressToEdit.name, [Validators.required, Validators.maxLength(this.nameMaxLength)]),
      surName: new FormControl(this.userAddressToEdit.surName, [Validators.required, Validators.maxLength(this.surNameMaxLength)]),
      street: new FormControl(this.userAddressToEdit.street, [Validators.required, Validators.maxLength(this.streetMaxLength)]),
      postCode: new FormControl(this.userAddressToEdit.postCode, [Validators.required, Validators.maxLength(this.postCodeMaxLength)]),
      phone: new FormControl(this.userAddressToEdit.phone, [Validators.required, Validators.pattern(this.phonePattern)]),
      email: new FormControl(this.userAddressToEdit.email, [Validators.required, Validators.pattern(this.emailPattern)]),
      city: new FormControl(this.userAddressToEdit.city, [Validators.maxLength(this.cityMaxLength)]),
      country: new FormControl(this.userAddressToEdit.country, [Validators.maxLength(this.countryMaxLength)])
    });
  }

  editUserAddress(): void {
    const model: UserAddressInfoForEdition = this.editAddressForm.value;

    this.userService.editUserAddress(this.userAddressToEdit.id, this.authService.decodedToken.nameid, model).subscribe(() => {
      this.messagePopup.displaySuccess(`Successfully edited user Address"${this.userAddressToEdit.id}`);
      this.editionMode.emit(false);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }


  cancel(): void {
    this.editionMode.emit(false);
  }

}
