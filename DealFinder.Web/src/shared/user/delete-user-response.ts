import { User } from "./user";
import { Error } from "../communication/error";

export class DeleteUserResponse {
    error: Error;
    hasError: boolean;
    user: User;
}