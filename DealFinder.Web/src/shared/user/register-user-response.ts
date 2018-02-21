import { Error } from "../communication/error";
import { User } from "./user";

export class RegisterUserResponse {
    error: Error;
    hasError: boolean;
    user: User;
}