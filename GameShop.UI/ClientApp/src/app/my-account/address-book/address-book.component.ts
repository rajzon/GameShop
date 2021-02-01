import { Router } from '@angular/router';
import { UserAddressFromServer } from '../../_models/UserAddressFromServer';
import { MessagePopupService } from './../../_services/message-popup.service';
import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-address-book',
  templateUrl: './address-book.component.html',
  styleUrls: ['./address-book.component.css']
})
export class AddressBookComponent implements OnInit {
  model: UserAddressFromServer[];
  addressToEdit: UserAddressFromServer = <UserAddressFromServer>{};
  decodedToken = this.authService.decodedToken;
  creationMode: boolean;
  editionMode: boolean;

  constructor(private userService: UserService, private router: Router,
        private authService: AuthService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getUserAddresses();
  }

  getUserAddresses(): void {
    this.userService.getUserAddresses(this.decodedToken.nameid).subscribe((response: UserAddressFromServer[]) => {
      this.model = response;
      console.log(response);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  deleteUserAddress(id: number): void {
    this.userService.deleteUserAddress(id, this.decodedToken.nameid).subscribe(() => {
      this.messagePopup.displaySuccess(`Address:${id} deleted successfully`);
      this.refreshComponent();
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  canceledOrSuccessfullCreation(creationMode: boolean): void {
    this.creationMode = creationMode;
    this.refreshComponent();
  }

  canceledOrSuccessfullEdition(editionMode: boolean): void {
    this.editionMode = editionMode;
    this.refreshComponent();
  }

  private refreshComponent(): void {
    this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/account/address-book']);
     });
  }


}
