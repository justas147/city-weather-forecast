import { Component, OnInit, Inject } from '@angular/core';
import { CityListComponent } from '@components/city-list/city-list.component';
import { CityDeleteDetails } from '@models/city-delete-details';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CityService } from '@services/city.service';

@Component({
  selector: 'app-delete-confirmation-dialog',
  templateUrl: './delete-confirmation-dialog.component.html',
  styleUrls: ['./delete-confirmation-dialog.component.css']
})
export class DeleteConfirmationDialogComponent implements OnInit {

  constructor(
    private cityService: CityService,
    public dialogRef: MatDialogRef<CityListComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CityDeleteDetails
  ) { }

  ngOnInit(): void {
  }

  deleteCity(code: string): void {
    this.cityService.deleteCity(code).subscribe(
      () => {
        this.dialogRef.close(true);
      },
      error => {
        console.log(error);
      }
    );
  }

  deleteAllCities(): void {
    this.cityService.deleteAllCities().subscribe(
      () => {
        this.dialogRef.close(true);
      },
      error => {
        console.log(error);
      }
    );
  }
}
