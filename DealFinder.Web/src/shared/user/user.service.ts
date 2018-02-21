import { Injectable } from "@angular/core";
import { UserRepository } from "./user.repository";

@Injectable()
export class UserService {
    private _userRepository: UserRepository;

    constructor(userRepository: UserRepository) {
        this._userRepository = userRepository;

    }

    registerUser(userToken: string, authenticator: string) {
        return new Promise((resolve, reject) => {
            let request = {
                userToken: userToken,
                authenticator: authenticator
            };
            this._userRepository.registerUser(request)
            .subscribe((payload) => {
                resolve(payload);
            }, (error) => {
                reject(error);
            });
        });
    }
}