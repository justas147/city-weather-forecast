import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteConfirmationDialogComponent } from '@components/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { CityService } from '@services/city.service';
import { CityDetails } from '@models/city-details';


@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.css']
})
export class CityListComponent implements OnInit {
  cities: CityDetails[];
  displayedColumns: string[] = ['name', 'max', 'min', 'button'];

  constructor(
    private cityService: CityService,
    private router: Router,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.cityService.getCities().subscribe(cities => {
      this.cities = cities;
    }, error => console.log(error));
  }

  openDialog(cityName: string, cityCode: string, toDeleteAll: boolean): void {
    const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, {
      width: '700px',
      data: {name: cityName, code: cityCode, deleteAll: toDeleteAll}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.cityService.getCities().subscribe(cities => {
          this.cities = cities;
        }, error => console.log(error));
      }
    });
  }
}
