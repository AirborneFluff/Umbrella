import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FilterDefinition } from '../filter-definition';
import { QueryParameter } from '../filter-option';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public getFilter(entityName: FilterDefinition) {
    return this.http.get<QueryParameter[]>(this.baseUrl + 'filters/' + entityName);
  }
}
