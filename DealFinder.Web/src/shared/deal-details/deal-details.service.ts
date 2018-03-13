import { EventEmitter, Injectable, Output } from "@angular/core";

@Injectable()
export class DealDetailsService {
    @Output() directionsRequired: EventEmitter<any> = new EventEmitter();

    requireDirections(destinationCoordinates: any) {
        this.directionsRequired.emit(destinationCoordinates);
    }
}