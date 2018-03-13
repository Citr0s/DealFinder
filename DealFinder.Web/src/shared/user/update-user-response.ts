import { User } from "./user";
import { Error } from "../communication/error";

export class UpdateUserResponse {
    error: Error;
    hasError: boolean;
    user: User;
}