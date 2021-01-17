import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { City } from '@models/city';
import { CityService } from '@services/city.service';

@Component({
  selector: 'app-city-edit-form',
  templateUrl: './city-edit-form.component.html',
  styleUrls: ['./city-edit-form.component.css']
})
export class CityEditFormComponent implements OnInit {
  id: string;
  
  editCityForm = new FormGroup({
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

  get code() { return this.editCityForm.get('placeCode').value }
  get name() { return this.editCityForm.get('name').value; }
  get description() { return this.editCityForm.get('description').value; }
  
  constructor(
    private cityService: CityService,    
    private route: ActivatedRoute,
    private router: Router 
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    this.cityService.getCity(this.id).subscribe(cityToEdit => {

      this.editCityForm.patchValue({
        placeCode: cityToEdit.placeCode,
        name: cityToEdit.name,
        description: cityToEdit.description
      })

      this.editCityForm.get('name').disable();
    }, 
    error => console.log(error));
  }

  editCity() {
    let newCity: City = { 
      placeCode: this.code, 
      name: this.name,
      description: this.description
    }

    this.cityService.editCity(this.id, newCity).subscribe(cities => {
      this.router.navigate(['cities']);
    }, error => console.log(error));
  }
}
