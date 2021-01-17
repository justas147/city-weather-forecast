import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CitySelection } from '@models/city-selection';
import { City } from '@models/city';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { untilDestroyed } from 'ngx-take-until-destroy';
import { CityService } from '@services/city.service';

@Component({
  selector: 'app-city-form',
  templateUrl: './city-form.component.html',
  styleUrls: ['./city-form.component.css']
})
export class CityFormComponent implements OnInit {
  options: CitySelection[];
  filteredOptions: Observable<CitySelection[]>;
  visibleOptions: number = 4;
  private originalOptions: CitySelection[] = [];
  
  cityForm = new FormGroup({
    placeCode: new FormControl(null, [
      Validators.required
    ]),
    name: new FormControl(null, [
      Validators.required
    ]),
    description: new FormControl('', [
      Validators.maxLength(255)
    ]),
  });

  get code() { return this.cityForm.get('placeCode'); }
  get name() { return this.cityForm.get('name'); }
  get description() { return this.cityForm.get('description'); }
  
  constructor(
    private cityService: CityService,    
    private router: Router 
  ) { }

  ngOnInit(): void {
    this.cityService.getCitySelection().subscribe(cities => {
      this.options = cities;
      this.originalOptions = [...this.options];

      this.cityForm.get('name').valueChanges
        .pipe(
          debounceTime(300),
          untilDestroyed(this)
        )
        .subscribe(term => {
          this.search(term);
        });
    }, error => {
      console.log(error);
      this.router.navigate(['cities']);
    });
  }

  postCity(): void {
    if(!this.isInputValid(this.name.value)){
      return;
    }

    let newCity: City = { 
      placeCode: this.code.value, 
      name: this.name.value,
      description: this.description.value
    }

    this.cityService.addCity(newCity).subscribe(() => {
      this.router.navigate(['cities']);
    }, error => {
      console.log(error);
      this.router.navigate(['cities']);
    });
  }

  select(option: CitySelection): void {
    this.cityForm.get('placeCode').setValue(option.placeCode);
    this.cityForm.get('name').setValue(option.name);
  }

  resetSelection(): void {
    this.cityForm.get('placeCode').setValue(null);
  }

  search(value: string): void {
    let input = value.toLowerCase();
    this.options = this.originalOptions.filter(option => option.name.toLowerCase().startsWith(input));
  }

  isInputValid(inputName: string): boolean {
    let newCityCheck: CitySelection = this.originalOptions.find(option => option.name === inputName);

    if(newCityCheck == null || newCityCheck.placeCode != this.code.value){
      this.resetSelection();
      return false;
    }

    return true;
  }

  validationCheck(): boolean {
    if((this.name.invalid && (this.name.dirty || this.name.touched)) || this.code.invalid) {
      return true;
    } else {
      return false;
    }
  }

  ngOnDestroy(): void {}
}
