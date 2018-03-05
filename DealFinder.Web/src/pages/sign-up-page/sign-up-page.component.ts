import {Component} from '@angular/core';
import {AuthService, FacebookLoginProvider, GoogleLoginProvider} from 'angular5-social-login';
import {UserService} from '../../shared/user/user.service';
import {Router} from '@angular/router';

@Component({
    selector: 'sign-up-page',
    templateUrl: './sign-up-page.component.html',
    styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
    private _authService: AuthService;
    private _userService: UserService;
    private _router: Router;

    constructor(authService: AuthService, userService: UserService, router: Router) {
        this._authService = authService;
        this._userService = userService;
        this._router = router;

        let isUserLoggedIn = this._userService.isLoggedIn();

        if (isUserLoggedIn)
            this._router.navigate(['']);
    }

    public socialSignIn(socialPlatform: string) {
        let socialPlatformProvider;
        if (socialPlatform == 'facebook') {
            socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
        } else if (socialPlatform == 'google') {
            socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
        }

        this._authService.signIn(socialPlatformProvider)
            .then((userData) => {
                    this._userService.registerUser(userData.idToken, socialPlatform)
                        .then(() => {
                            this._router.navigate(['']);
                        })
                        .catch(() => {
                            // TODO: display error
                        });
                }
            );
    }

    replaceSrc(source: string) {
        let element = document.querySelector('img');
        element.setAttribute('src', source);
    }
}