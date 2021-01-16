import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CityDetails } from '@models/city-details';
import { CitySelection } from '@models/city-selection';
import { City } from '@models/city';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { CityService } from '@services/city.service';

@Component({
  selector: 'app-city-form',
  templateUrl: './city-form.component.html',
  styleUrls: ['./city-form.component.css']
})
export class CityFormComponent implements OnInit {
  id: string;
  city: CityDetails;

  options: CitySelection[];

  filteredOptions: Observable<CitySelection[]>;
  
  cityForm = new FormGroup({
    city: new FormControl(null, [
      Validators.required
    ]),
    description: new FormControl('', [
      Validators.maxLength(255)
    ]),
  });

  get code() { return this.cityForm.get('city').value.placeCode; }
  get name() { return this.cityForm.get('city').value.name; }
  get description() { return this.cityForm.get('description').value; }
  
  constructor(
    private cityService: CityService,    
    private route: ActivatedRoute,
    private router: Router 
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    if(this.id !== null) {
      // this.movieService.getMovie(this.id).subscribe(city => {
      //   this.city = city;
      //   let cityToEdit: CitySelection = { PlaceCode: this.city.code, Name: this.city.name};

      //   this.cityForm.setValue({
      //     city: cityToEdit,
      //     description: this.city.description
      //   })
      // }, 
      // error => console.log(error));
    } else {
      this.cityService.getCitySelection().subscribe(cities => {
        this.options = cities;
        this.filteredOptions = this.cityForm.get('city').valueChanges
        .pipe(
          startWith(''),
          map(value => typeof value === 'string' ? value : value.Name),
          map(name => name ? this._filter(name) : this.options.slice())
        );
      }, error => console.log(error));
    }
  }

  displayFn(city: CitySelection): string {
    return city && city.name ? city.name : '';
  }

  private _filter(name: string): CitySelection[] {
    const filterValue = name.toLowerCase();
    return this.options.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }

  addCity() {
    let newCity: City = { 
      placeCode: this.code, 
      name: this.name,
      description: this.description
    }

    this.cityService.addCity(newCity).subscribe(cities => {
      this.router.navigate(['cities']);
    }, error => console.log(error));
  }
}
