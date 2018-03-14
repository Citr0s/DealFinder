import { Component, EventEmitter, Inject, Output } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { DealDetailsService } from "../../../shared/deal-details/deal-details.service";

@Component({
    selector: 'deal-details-modal',
    templateUrl: 'deal-details-modal.html',
    styleUrls: ['./deal-details-modal.scss']
})
export class DealDetailsModal {
    dialogRef: MatDialogRef<DealDetailsModal>;
    @Output() directionsRequired: EventEmitter<any> = new EventEmitter();
    private _dealDetailsService: DealDetailsService;

    constructor(dialogRef: MatDialogRef<DealDetailsModal>, @Inject(MAT_DIALOG_DATA) public data: any, dealDetailsService: DealDetailsService) {
        this.dialogRef = dialogRef;
        this._dealDetailsService = dealDetailsService;
    }

    displayRoute(destinationCoordinates) {
        this._dealDetailsService.requireDirections(destinationCoordinates);
        this.dialogRef.close();
    }
}