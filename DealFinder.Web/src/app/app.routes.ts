import { Routes } from '@angular/router';
import { HomePageComponent } from "../pages/home-page/home-page.component";
import { NewDealPageComponent } from "../pages/new-deal-page/new-deal-page.component";

export const appRoutes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'new-deal',
        component: NewDealPageComponent
    }
];