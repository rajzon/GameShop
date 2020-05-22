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
    {path: 'sign-in' , component: SignInComponent, canActivate: [NegateAuthGuard]},
    {path: 'register' , component: RegisterComponent, canActivate: [NegateAuthGuard]},
    {path: 'admin' , component: AdminPanelComponent, canActivate: [AuthGuard], data: {roles: ['Admin','Moderator']}},
    {path: '**' , redirectTo: 'home' , pathMatch: 'full' },
];
