import { MessagePopupService } from './../_services/message-popup.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};


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
    console.log('cancled');
    this.router.navigate(['/home']);
  }

}
