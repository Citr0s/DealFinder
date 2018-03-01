import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";

@Component({
    selector: 'deal-details-modal',
    templateUrl: 'deal-details-modal.html',
    styleUrls: ['./deal-details-modal.scss']
})
export class DealDetailsModal {
    dialogRef: MatDialogRef<DealDetailsModal>;

    constructor(dialogRef: MatDialogRef<DealDetailsModal>, @Inject(MAT_DIALOG_DATA) public data: any) {
        this.dialogRef = dialogRef;
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

}