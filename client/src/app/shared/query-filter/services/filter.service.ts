import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FilterOption } from '../filter-option';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FilterDefinition } from '../filter-definition';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public getFilter(entityName: FilterDefinition) {
    return this.http.get<FilterOption[]>(this.baseUrl + 'filters/' + entityName);
  }
}
