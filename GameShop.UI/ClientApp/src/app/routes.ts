import { AddressBookComponent } from './my-account/address-book/address-book.component';
import { ChangeUserInfoComponent } from './my-account/change-user-info/change-user-info.component';
import { CheckoutOrderinfoGuard } from './_guards/checkout-orderinfo.guard';
import { CheckoutPaymentGuard } from './_guards/checkout-payment.guard';
import { PaymentComponent } from './checkout/payment/payment.component';
import { OrderInfoComponent } from './checkout/order-info/order-info.component';
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
    {path: 'account/change-user-info' , component: ChangeUserInfoComponent, canActivate: [AuthGuard] },
    {path: 'account/address-book' , component: AddressBookComponent, canActivate: [AuthGuard] },
    {path: 'basket' , component: BasketComponent },
    {path: 'contact' , component: ContactComponent },
    {path: 'product/:id' , component: ProductCardComponent },
    {path: 'checkout/order-info' , component: OrderInfoComponent, canActivate: [CheckoutOrderinfoGuard]},
    {path: 'checkout/payment' , component: PaymentComponent, canActivate: [CheckoutPaymentGuard, CheckoutOrderinfoGuard]},
    {path: 'sign-in' , component: SignInComponent, canActivate: [NegateAuthGuard]},
    {path: 'register' , component: RegisterComponent, canActivate: [NegateAuthGuard]},
    {path: 'admin' , component: AdminPanelComponent, canActivate: [AuthGuard], data: {roles: ['Admin', 'Moderator']}},
    {path: '**' , redirectTo: 'home' , pathMatch: 'full' },
];
