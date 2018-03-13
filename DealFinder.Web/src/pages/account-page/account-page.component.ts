import { Component } from '@angular/core';
import { UserService } from "../../shared/user/user.service";
import { UpdateUserResponse } from "../../shared/user/update-user-response";
import { Router } from "@angular/router";

@Component({
    selector: 'account-page',
    templateUrl: './account-page.component.html',
    styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent {
    private username: string;
    private _userService: UserService;
    private errorMessage: string;
    private successMessage: string;
    private _router: Router;

    constructor(userService: UserService, router: Router) {
        this._userService = userService;
        this._router = router;

        let isUserLoggedIn = this._userService.isLoggedIn();
        if (!isUserLoggedIn)
            this._router.navigate(['']);

        this.username = this._userService.getPersistedUser().username;
    }

    updateUsernameValue(event) {
        this.username = event.srcElement.value;
    }

    submit() {
        let user = this._userService.getPersistedUser();
        user.username = this.username;

        this._userService.updateUser(user)
        .then((payload: UpdateUserResponse) => {
            if (payload.hasError) {
                this.errorMessage = payload.error.userMessage;
                return;
            }

            this._userService.persistUser(payload.user);
            this.successMessage = 'Account Updated Successfully';
            this.username = payload.user.username;
        });
    }
}