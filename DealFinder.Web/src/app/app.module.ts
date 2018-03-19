import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './app.routes';
import { RouterModule } from '@angular/router';
import { HomePageComponent } from '../pages/home-page/home-page.component';
import { DealsService } from '../shared/deals/deals.service';
import { DealsRepository } from '../shared/deals/deals.repository';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
    MatButtonModule,
    MatCardModule, MatChipsModule, MatDialogModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSidenavModule, MatSliderModule, MatTabsModule,
    MatToolbarModule
} from '@angular/material';
import { NewDealPageComponent } from '../pages/new-deal-page/new-deal-page.component';
import { SignUpPageComponent } from '../pages/sign-up-page/sign-up-page.component';
import {
    SocialLoginModule,
    AuthServiceConfig,
    GoogleLoginProvider,
} from 'angular5-social-login';
import { UserService } from '../shared/user/user.service';
import { UserRepository } from '../shared/user/user.repository';
import { AgmCoreModule } from "@agm/core";
import { LocationService } from "../shared/location/location.service";
import { DealDetailsModal } from "../pages/home-page/deal-details/deal-details.modal";
import { VoteService } from "../shared/vote/vote.service";
import { VoteRepository } from "../shared/vote/vote.repository";
import { ServiceWorkerModule } from "@angular/service-worker";
import { environment } from "../environments/environment";
import { MomentModule } from "angular2-moment";
import { OnlyVisibleFilter } from "../pages/home-page/deals.filter";
import { AccountPageComponent } from "../pages/account-page/account-page.component";
import { AgmDirectionModule } from "agm-direction/src/agm-direction.module";
import { DealDetailsService } from "../shared/deal-details/deal-details.service";
import {ReCaptchaModule} from 'angular2-recaptcha';

export function getAuthServiceConfigs() {
    return new AuthServiceConfig(
        [
            {
                id: GoogleLoginProvider.PROVIDER_ID,
                provider: new GoogleLoginProvider('335909346629-qaf57344qo332figcf662t3c7hgndtk2')
            },
        ]
    );
}

@NgModule({
    declarations: [
        AppComponent,
        HomePageComponent,
        NewDealPageComponent,
        SignUpPageComponent,
        DealDetailsModal,
        OnlyVisibleFilter,
        AccountPageComponent
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
        MatFormFieldModule,
        MatProgressSpinnerModule,
        MatTabsModule,
        MatDialogModule,
        MatSliderModule,
        MatChipsModule,
        SocialLoginModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyAHYBwqaOLSFxAQvK439xrVHIL7Tp_fobk'
        }),
        AgmDirectionModule,
        ServiceWorkerModule.register('/ngsw-worker.js', {
            enabled: environment.production
        }),
        MomentModule,
        ReCaptchaModule
    ],
    providers: [
        DealsService,
        DealsRepository,
        {
            provide: AuthServiceConfig,
            useFactory: getAuthServiceConfigs
        },
        UserService,
        UserRepository,
        LocationService,
        VoteService,
        VoteRepository,
        DealDetailsService
    ],
    bootstrap: [AppComponent],
    entryComponents: [
        DealDetailsModal
    ]
})
export class AppModule {
}
