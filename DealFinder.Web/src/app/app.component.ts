import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from "@angular/material";
import { UserService } from "../shared/user/user.service";
import { User } from "../shared/user/user";
import { Router } from "@angular/router";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    @ViewChild('sidenav') sidenav: MatSidenav;
    private _userService: UserService;
    isUserLoggedIn: boolean;
    user: User;
    private _router: Router;

    constructor(userService: UserService, router: Router) {
        this._userService = userService;
        this._router = router;

        this.checkUserStatus();

        this._userService.onChange
        .subscribe(() => {
            this.checkUserStatus();
        });
    }

    close() {
        this.sidenav.close();
    }

    logout() {
        this._userService.logOut();
        this._router.navigate(['']);
        this.close();
    }

    private checkUserStatus() {
        this.isUserLoggedIn = this._userService.isLoggedIn();
        this.user = this._userService.getPersistedUser();
    }
}
