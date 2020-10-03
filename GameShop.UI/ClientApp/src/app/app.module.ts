import { PaymentComponent } from './checkout/payment/payment.component';
import { CustomerInfoComponent } from './checkout/customer-info/customer-info.component';
import { StockManagmentComponent } from './admin/stock-managment/stock-managment.component';
import { ShopSearchingService } from './_services/shop-searching.service';
import { EditProductComponent } from './admin/edit-product/edit-product.component';
import { RequirementsModalComponent } from './admin/requirements-modal/requirements-modal.component';
import { CreateProductComponent } from './admin/create-product/create-product.component';
import { AdminService } from './_services/admin.service';
import { ProductManagementComponent } from './admin/product-management/product-management.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AuthService } from './_services/auth.service';
import { appRoutes } from './routes';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgSelectModule } from '@ng-select/ng-select';
import { CookieService } from 'ngx-cookie-service';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { MyAccountComponent } from './my-account/my-account.component';
import { BasketComponent } from './basket/basket.component';
import { ContactComponent } from './contact/contact.component';
import { RegisterComponent } from './register/register.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ProductCardComponent } from './home/product-card/product-card.component';



export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      NavComponent,
      MyAccountComponent,
      BasketComponent,
      ContactComponent,
      RegisterComponent,
      SignInComponent,
      AdminPanelComponent,
      HasRoleDirective,
      UserManagementComponent,
      ProductManagementComponent,
      RolesModalComponent,
      CreateProductComponent,
      EditProductComponent,
      RequirementsModalComponent,
      ProductCardComponent,
      StockManagmentComponent,
      CustomerInfoComponent,
      PaymentComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      PaginationModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      ModalModule.forRoot(),
      NgSelectModule,
      FileUploadModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
  ],
  providers: [
     ErrorInterceptorProvider,
     AuthService,
     AdminService,
     ShopSearchingService,
     CookieService
  ],
  entryComponents: [
     RolesModalComponent,
     RequirementsModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
