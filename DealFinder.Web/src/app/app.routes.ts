import { Routes } from '@angular/router';
import { HomePageComponent } from "../pages/home-page/home-page.component";
import { NewDealPageComponent } from "../pages/new-deal-page/new-deal-page.component";
import { SignUpPageComponent } from "../pages/sign-up-page/sign-up-page.component";
import { AccountPageComponent } from "../pages/account-page/account-page.component";

export const appRoutes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'new-deal',
        component: NewDealPageComponent
    },
    {
        path: 'sign-up',
        component: SignUpPageComponent
    },
    {
        path: 'account',
        component: AccountPageComponent
    }
];