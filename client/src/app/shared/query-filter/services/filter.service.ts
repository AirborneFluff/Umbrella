import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FilterDefinition } from '../filter-definition';
import { QueryParameter } from '../filter-option';
import { QueryFilter } from '../query-filter';
import { map, Observable, of, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private baseUrl = environment.apiUrl;
  private filterInstances: QueryFilter[] = [];

  constructor(private http: HttpClient) { }

  private getFilter(entityName: FilterDefinition) {
    return this.http.get<QueryParameter[]>(this.baseUrl + 'filters/' + entityName);
  }

  private createFilter(entityName: FilterDefinition, parameters: QueryParameter[]): QueryFilter {
    const filter = new QueryFilter(parameters, entityName);
    this.filterInstances.push(filter);
    return filter;
  }

  public getFilterInstance(entityName: FilterDefinition): Observable<QueryFilter> {
    const index = this.filterInstances.findIndex(filter => filter.entityName == entityName);
    if (index != -1) return of(this.filterInstances[index]);

    return this.getFilter(entityName).pipe(
      take(1),
      map(params => this.createFilter(entityName, params)));
  }
}
