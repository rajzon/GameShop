import { MessagePopupService } from './../../_services/message-popup.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from './../../_services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderInfo } from 'src/app/_models/OrderInfo';
import { environment } from 'src/environments/environment';
import { IsPhoneNumber } from 'src/app/_validators/IsPhoneNumber';
import { IsEmailAddress } from 'src/app/_validators/IsEmailAddress';
import { UserAddressFromServer } from 'src/app/_models/UserAddressFromServer';
import { morphism, createSchema } from 'morphism';
import { MapperConfig } from 'src/app/_mappings/MapperConfig';

@Component({
  selector: 'app-edit-address-checkout',
  templateUrl: './edit-address-checkout.component.html',
  styleUrls: ['./edit-address-checkout.component.css'],
})

export class EditAddressCheckoutComponent implements OnInit {
  @Input() orderInfo: OrderInfo;
  @Output() editionMode = new EventEmitter();
  addressBookOpt: boolean;
  userAddressBook: UserAddressFromServer[];

  selectedAddressBookElId: number;

  decodedToken = this.authService.decodedToken;

  editAddressForm: FormGroup;

  phonePattern = environment.phonePattern;
  emailPattern = environment.emailPattern;
  nameMaxLength = environment.nameMaxLength;
  surNameMaxLength = environment.surNameMaxLength;
  streetMaxLength = environment.streetMaxLength;
  postCodeMaxLength = environment.postCodeMaxLength;
  cityMaxLength = environment.cityMaxLength;
  countryMaxLength = environment.countryMaxLength;

  constructor(private userService: UserService, private authService: AuthService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.selectedAddressBookElId = JSON.parse(localStorage.getItem('selectedAddressBookElId'));
    this.initForm();
    this.getUserAddresses();
  }

  initForm(): void {
    this.editAddressForm = new FormGroup({
      name: new FormControl(this.orderInfo.name, [Validators.required, Validators.maxLength(this.nameMaxLength)]),
      surName: new FormControl(this.orderInfo.surName, [Validators.required, Validators.maxLength(this.surNameMaxLength)]),
      street: new FormControl(this.orderInfo.street, [Validators.required, Validators.maxLength(this.streetMaxLength)]),
      postCode: new FormControl(this.orderInfo.postCode, [Validators.required, Validators.maxLength(this.postCodeMaxLength)]),
      phone: new FormControl(this.orderInfo.phone, [Validators.required, IsPhoneNumber()]),
      email: new FormControl(this.orderInfo.email, [Validators.required, IsEmailAddress()]),
      city: new FormControl(this.orderInfo.city, [Validators.maxLength(this.cityMaxLength)]),
      country: new FormControl(this.orderInfo.country, [Validators.maxLength(this.countryMaxLength)])
    });
  }

  cancel(): void {
    this.editionMode.emit(false);
  }


  getUserAddresses(): void {
    if (this.decodedToken == null) {
      return;
    }

    this.userService.getUserAddresses(this.decodedToken.nameid).subscribe((response: UserAddressFromServer[]) => {
      this.userAddressBook = response;
      console.log(response);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }


  submitCustomerAddress(): void {
    const deliveryType = this.orderInfo.deliveryType;
    this.orderInfo = this.editAddressForm.value;
    (deliveryType !== null)? this.orderInfo.deliveryType = deliveryType: {};
    const orderInfo = JSON.stringify(this.orderInfo);
    localStorage.setItem('orderInfo', orderInfo);
    this.editionMode.emit(false);
  }

  applyDataFromAddressBook(): void {
    const userAddressToApply = this.userAddressBook.find(x => x.id === this.selectedAddressBookElId);
    const prevOrderInfo = localStorage.getItem('orderInfo');
    console.log(prevOrderInfo);

    const orderInfoSchema = MapperConfig.userAddressFromServerToOrderInfoScheme;
    const newOrderInfo = <OrderInfo>morphism(orderInfoSchema, userAddressToApply);
    newOrderInfo.deliveryType = this.orderInfo.deliveryType;
    console.log(newOrderInfo);
    localStorage.setItem('orderInfo', JSON.stringify(newOrderInfo));
    localStorage.setItem('selectedAddressBookElId', JSON.stringify(this.selectedAddressBookElId) );

    if (prevOrderInfo != JSON.stringify(newOrderInfo)) {
      this.messagePopup.displaySuccess("Successfully edited orderInfo based on address book");
    } else {
      this.messagePopup.displayInfo("Address wasn't changed");
    }
    this.editionMode.emit(false);
    
  }

}
