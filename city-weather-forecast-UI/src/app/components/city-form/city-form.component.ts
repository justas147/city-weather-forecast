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

  get code() { return this.cityForm.get('placeCode').value }
  get name() { return this.cityForm.get('name').value; }
  get description() { return this.cityForm.get('description').value; }
  
  constructor(
    private cityService: CityService,    
    private router: Router 
  ) { }

  ngOnInit() {
    this.cityService.getCitySelection().subscribe(cities => {
      this.options = cities;
      this.originalOptions = [...this.options];

      this.cityForm.get('name').valueChanges
        .pipe(
          debounceTime(300),
          untilDestroyed(this)
        )
        .subscribe(term => {
          this.removeFormValue();
          this.search(term)
        });
    }, error => {
      
      console.log(error)
    });
  }

  postCity(): void {
    let newCity: City = { 
      placeCode: this.code, 
      name: this.name,
      description: this.description
    }

    this.cityService.addCity(newCity).subscribe(cities => {
      this.router.navigate(['cities']);
    }, error => console.log(error));
  }

  select(option: CitySelection): void {
    this.cityForm.get('placeCode').setValue(option.placeCode);
    this.cityForm.get('name').setValue(option.name);
  }

  isActive(option: CitySelection): boolean {
    if (!option) {
      return false;
    }

    return option.placeCode === this.code;
  }

  search(value: string): void {
    let input = value.toLowerCase();
    this.options = this.originalOptions.filter(option => option.name.toLowerCase().startsWith(input));
  }

  removeFormValue(): void{
    this.cityForm.get('placeCode').reset();
  }

  ngOnDestroy() {}
}
