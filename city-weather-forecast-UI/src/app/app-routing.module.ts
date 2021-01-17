import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CityListComponent } from '@components/city-list/city-list.component';
import { CityFormComponent } from '@components/city-form/city-form.component';
import { CityEditFormComponent } from '@components/city-edit-form/city-edit-form.component';

const routes: Routes = [
  {path: 'cities', component: CityListComponent},
  {path: 'cities/form', component: CityFormComponent},
  {path: 'cities/form/:id', component: CityEditFormComponent},
  {path: '', redirectTo: 'cities', pathMatch: 'full'}, // default page
  {path: '**', component: CityListComponent} // 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
