import { AuthService } from './../_services/auth.service';
import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible = false;
  jwtHelper = new JwtHelperService();
  subscription: Subscription;

  constructor(private viewContainerRef: ViewContainerRef,
               private templateRef: TemplateRef<any>,
               private authService: AuthService ) { }

  ngOnInit() {
    this.subscription = this.authService.activateHasRoleDirective().subscribe(x => {
      console.log('DecodedToken From hasRoleDirective  passed by sign-in component or nav-bar component' + this.authService.decodedToken);
      console.log(localStorage.getItem('token'));

      if (!this.isUserAuthenticated()) { return; }
      this.userAuthorization();

    });
    console.log('TEST2');
    // it guard the DOM from unauthorised users during refreshing and navigating
    if (this.authService.loggedIn()) {
      console.log('DecodedToken From hasRoleDirective during refresing page or navigating to protected elements'
                     + this.authService.decodedToken);
      console.log(localStorage.getItem('token'));
      if (!this.isUserAuthenticated()) { return; }
      this.userAuthorization();

    }


  }

  isUserAuthenticated(): boolean {
    if (!this.authService.decodedToken) {
      this.isVisible = false;
      this.viewContainerRef.clear();
      return false;
    }

    return true;
  }

  userAuthorization(): void {

      console.log(this.authService.decodedToken);
      const userRoles = this.authService.decodedToken.role as Array<string>;
      console.log(userRoles);
      if (!userRoles) {
        this.viewContainerRef.clear();
      }
      console.log(this.isVisible);
      if (this.authService.roleMatch(this.appHasRole)) {
        if (!this.isVisible) {
          this.isVisible = true;
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          this.isVisible = false;
          this.viewContainerRef.clear();
        }

    }

  }

}
