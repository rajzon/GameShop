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
      if (!this.isUserAuthenticated()) { return; }
      this.userAuthorization();

    });
    // it guard the DOM from unauthorised users during refreshing and navigating
    if (this.authService.loggedIn()) {
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
      const userRoles = this.authService.decodedToken.role as Array<string>;
      if (!userRoles) {
        this.viewContainerRef.clear();
      }
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
