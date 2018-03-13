import { EventEmitter, Injectable, Output } from "@angular/core";
import { UserRepository } from "./user.repository";
import { RegisterUserResponse } from "./register-user-response";
import { User } from "./user";
import { UpdateUserResponse } from "./update-user-response";
import { DeleteUserResponse } from "./delete-user-response";

@Injectable()
export class UserService {
    @Output() onChange: EventEmitter<any> = new EventEmitter();
    private _userRepository: UserRepository;

    constructor(userRepository: UserRepository) {
        this._userRepository = userRepository;

    }

    registerUser(userToken: string, authenticator: string): Promise<RegisterUserResponse> {
        return new Promise((resolve, reject) => {
            let request = {
                userToken: userToken,
                authenticator: authenticator
            };
            this._userRepository.registerUser(request)
            .subscribe((payload: RegisterUserResponse) => {
                this.persistUser(payload.user);
                resolve(payload);
            }, (error) => {
                reject(error);
            });
        });
    }

    persistUser(payload: User) {
        localStorage.setItem('user', JSON.stringify(payload));
        this.onChange.emit(payload);
    }

    getPersistedUser() {
        return JSON.parse(localStorage.getItem('user'));
    }

    isLoggedIn() {
        return localStorage.getItem('user') !== null;
    }

    logOut() {
        localStorage.removeItem('user');
        this.onChange.emit();
    }

    updateUser(user: User) {
        return new Promise((resolve, reject) => {
            let request = {
                user: user
            };
            this._userRepository.updateUser(request)
            .subscribe((payload: UpdateUserResponse) => {
                this.persistUser(payload.user);
                resolve(payload);
            }, (error) => {
                reject(error);
            });
        });
    }

    deleteAccount(userIdentifier: string): Promise<DeleteUserResponse> {
        return new Promise((resolve, reject) => {
            this._userRepository.deleteUser(userIdentifier)
            .subscribe((payload: DeleteUserResponse) => {
                resolve(payload);
            }, (error) => {
                reject(error);
            });
        });
    }
}