import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CityDetails } from '@models/city-details';
import { CitySelection } from '@models/city-selection';
import { Observable } from 'rxjs';

const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Accept': 'application/json'
});

@Injectable({
  providedIn: 'root'
})
export class CityService {
  private cityApiUrl = 'https://localhost:5001/api/cities';

  constructor(private http: HttpClient) { }

  getCities(): Observable<CityDetails[]> {
    return this.http.get<CityDetails[]>(this.cityApiUrl);
  }

  getCity(cityCode: string): Observable<CityDetails> {
    return this.http.get<CityDetails>(`${this.cityApiUrl}/${cityCode}`);
  }

  getCitySelection(): Observable<CitySelection[]> {
    return this.http.get<CitySelection[]>(`${this.cityApiUrl}/selection`);
  }

  addCity(cityData: Object): Observable<any> {
    return this.http.post<any>(this.cityApiUrl, cityData, {headers});
  }

  editCity(cityCode: string, cityData: Object): Observable<any> {
    return this.http.put<any>(`${this.cityApiUrl}/${cityCode}`, cityData, {headers})
  }

  deleteCity(cityCode: string) {
    return this.http.delete<boolean>(`${this.cityApiUrl}/${cityCode}`);
  }

  deleteAllCities() {
    return this.http.delete<boolean>(`${this.cityApiUrl}/`);
  }
}
