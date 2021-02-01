import { MessagePopupService } from './../_services/message-popup.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  emailPattern = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$';
  phonePattern = '^[0-9]{1,15}$';

  userNameMaxLength: number = environment.userNameMaxLength;
  userNameMinLength: number = environment.userNameMinLength;
  userSurNameMaxLength: number = environment.userSurNameMaxLength;
  userSurNameMinLength: number = environment.userSurNameMinLength;
  userPasswordMaxLength: number = environment.userPasswordMaxLength;
  userPasswordMinLength: number = environment.userPasswordMinLength;

  constructor(private authService: AuthService, private router: Router, private messagePopup: MessagePopupService) { }

  ngOnInit() {
  }

  register(): void {
    console.log(this.model);
    this.authService.register(this.model).subscribe(() => {
      this.messagePopup.displaySuccess('Successful registration');
      this.router.navigate(['/home']);
    }, error => {
      console.log(error);
      this.messagePopup.displayError(error);
    });

  }

  cancel(): void {
    this.router.navigate(['/home']);
  }

}
