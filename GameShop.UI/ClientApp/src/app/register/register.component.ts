import { MessagePopupService } from './../_services/message-popup.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  userNameMaxLength: number = environment.userNameMaxLength;
  userNameMinLength: number = environment.userNameMinLength;
  userPasswordMaxLength: number = environment.userPasswordMaxLength;
  userPasswordMinLength: number = environment.userPasswordMinLength;

  constructor(private authService: AuthService, private router: Router, private messagePopup: MessagePopupService) { }

  ngOnInit() {
  }

  register(): void {
    this.authService.register(this.model).subscribe(() => {
      this.messagePopup.displaySuccess('Successful registration');
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  cancel(): void {
    this.router.navigate(['/home']);
  }

}
