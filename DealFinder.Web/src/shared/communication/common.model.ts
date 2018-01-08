export class CommonModel {
    hasError: boolean;
    error: string;

    constructor() {
        this.hasError = false;
    }

    public addError(error: string) {
        this.hasError = true;
        this.error = error;
    }
}