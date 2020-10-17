import { CheckoutCustomerinfoGuard } from './_guards/checkout-customerinfo.guard';
import { CheckoutPaymentGuard } from './_guards/checkout-payment.guard';
import { PaymentComponent } from './checkout/payment/payment.component';
import { CustomerInfoComponent } from './checkout/customer-info/customer-info.component';
import { ProductCardComponent } from './home/product-card/product-card.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { NegateAuthGuard } from './_guards/negate-auth.guard';

import { AuthGuard } from './_guards/auth.guard';
import { ContactComponent } from './contact/contact.component';
import { BasketComponent } from './basket/basket.component';
import { HomeComponent } from './home/home.component';
import { MyAccountComponent } from './my-account/my-account.component';
import { Routes } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { RegisterComponent } from './register/register.component';

export const appRoutes: Routes = [
    {path: 'home' , component: HomeComponent },
    {path: 'account' , component: MyAccountComponent, canActivate: [AuthGuard] },
    {path: 'basket' , component: BasketComponent },
    {path: 'contact' , component: ContactComponent },
    {path: 'product/:id' , component: ProductCardComponent },
    {path: 'checkout/customer-info' , component: CustomerInfoComponent, canActivate: [CheckoutCustomerinfoGuard]},
    {path: 'checkout/payment' , component: PaymentComponent, canActivate: [CheckoutPaymentGuard, CheckoutCustomerinfoGuard]},
    {path: 'sign-in' , component: SignInComponent, canActivate: [NegateAuthGuard]},
    {path: 'register' , component: RegisterComponent, canActivate: [NegateAuthGuard]},
    {path: 'admin' , component: AdminPanelComponent, canActivate: [AuthGuard], data: {roles: ['Admin', 'Moderator']}},
    {path: '**' , redirectTo: 'home' , pathMatch: 'full' },
];
