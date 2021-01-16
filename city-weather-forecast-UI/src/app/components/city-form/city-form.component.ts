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
      this.getCityToEdit();
    } else {
      this.getCitySelections();
    }
  }

  getCityToEdit(){
    this.cityService.getCity(this.id).subscribe(city => {
      this.city = city;
      let cityToEdit: CitySelection = { placeCode: this.city.placeCode, name: this.city.name};

      this.cityForm.setValue({
        city: cityToEdit,
        description: this.city.description
      })

      this.cityForm.get('city').disable();
    }, 
    error => console.log(error));
  }

  getCitySelections(){
    this.cityService.getCitySelection().subscribe(cities => {
      this.options = cities;
    }, error => console.log(error));
  }

  postCity() {
    let newCity: City = { 
      placeCode: this.code, 
      name: this.name,
      description: this.description
    }

    if(this.id !== null) {
      this.cityService.editCity(this.id, newCity).subscribe(cities => {
        this.router.navigate(['cities']);
      }, error => console.log(error));
    } else {
      this.cityService.addCity(newCity).subscribe(cities => {
        this.router.navigate(['cities']);
      }, error => console.log(error));
    }
  }
}
