import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './app.routes';
import { RouterModule } from '@angular/router';
import { HomePageComponent } from "../pages/home-page/home-page.component";
import { DealsService } from "../shared/deals/deals.service";
import { DealsRepository } from "../shared/deals/deals.repository";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
    MatButtonModule, MatCardModule, MatDividerModule, MatFormFieldModule, MatIconModule, MatInputModule,
    MatSidenavModule,
    MatToolbarModule
} from "@angular/material";
import { NewDealPageComponent } from "../pages/new-deal-page/new-deal-page.component";
import { SignUpPageComponent } from "../pages/sign-up-page/sign-up-page.component";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        NewDealPageComponent,
        SignUpPageComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),
        BrowserAnimationsModule,
        MatSidenavModule,
        MatToolbarModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        MatDividerModule,
        MatInputModule,
        MatFormFieldModule
    ],
    providers: [
        DealsService,
        DealsRepository
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
