import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './app.routes';
import { RouterModule } from '@angular/router';
import { HomePageComponent } from "../pages/home-page/home-page.component";
import { DealsService } from "../shared/deals/deals.service";
import { DealsRepository } from "../shared/deals/deals.repository";

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,

    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes)
    ],
    providers: [
        DealsService,
        DealsRepository
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
