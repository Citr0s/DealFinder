import { Component } from '@angular/core';
import { UserService } from "../../shared/user/user.service";
import { UpdateUserResponse } from "../../shared/user/update-user-response";
import { Router } from "@angular/router";
import { User } from "../../shared/user/user";
import { DealsService } from "../../shared/deals/deals.service";
import { DeleteUserResponse } from "../../shared/user/delete-user-response";

@Component({
    selector: 'account-page',
    templateUrl: './account-page.component.html',
    styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent {
    user: User;
    private _userService: UserService;
    private errorMessage: string;
    private successMessage: string;
    private _router: Router;
    private _dealsService: DealsService;

    constructor(userService: UserService, router: Router, dealsService: DealsService) {
        this._userService = userService;
        this._router = router;
        this._dealsService = dealsService;

        let isUserLoggedIn = this._userService.isLoggedIn();
        if (!isUserLoggedIn)
            this._router.navigate(['']);

        this.user = this._userService.getPersistedUser();
    }

    updateUsernameValue(event) {
        this.user.username = event.srcElement.value;
    }

    submit() {
        this._userService.updateUser(this.user)
        .then((payload: UpdateUserResponse) => {
            if (payload.hasError) {
                this.errorMessage = payload.error.userMessage;
                return;
            }

            this._userService.persistUser(payload.user);
            this.successMessage = 'Account Updated Successfully';
            this.user.username = payload.user.username;
        });
    }

    clearLocalStorage() {
        this._dealsService.removePersistedDeals();
        this._userService.logOut();

        this._router.navigate(['']);
    }

    deleteAccount() {
        this._userService.deleteAccount(this.user.identifier)
        .then((payload: DeleteUserResponse) => {
            if (payload.hasError) {
                this.errorMessage = payload.error.userMessage;
                return;
            }

            this._userService.logOut();
            this._router.navigate(['']);
        });
    }
}