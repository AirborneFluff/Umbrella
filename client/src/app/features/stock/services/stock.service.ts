import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { StockItem } from '../../../core/models/stock-item';
import { PaginatedResult } from '../../../core/utilities/pagination';
import { getPaginatedResult, getPaginationHeaders } from '../../../core/utilities/pagination-helper';
import { PagedSearchParams } from '../../../core/params/paged-search-params';
import { mergeParams } from '../../../core/utilities/http-params-helper';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private baseUrl = environment.apiUrl + 'stockItems/';

  constructor(private http: HttpClient) { }

  public getByPartCode(partCode: string): Observable<StockItem> {
    return this.http.get<StockItem>(this.baseUrl + partCode);
  }

  public getPaginatedList(stockParams: PagedSearchParams, additionalParams?: HttpParams): Observable<PaginatedResult<StockItem[]>> {
    let params = getPaginationHeaders(stockParams.pageNumber, stockParams.pageSize);

    if (additionalParams) {
      params = mergeParams(params, additionalParams);
    }

    if (stockParams.searchTerm) params = params.set('searchTerm', stockParams.searchTerm);
    return getPaginatedResult<StockItem[]>(this.baseUrl, params, this.http);
  }
}
