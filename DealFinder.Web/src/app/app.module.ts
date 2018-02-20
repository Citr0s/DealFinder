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
import { MatButtonModule, MatCardModule, MatIconModule, MatSidenavModule, MatToolbarModule } from "@angular/material";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,

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
        MatCardModule
    ],
    providers: [
        DealsService,
        DealsRepository
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
