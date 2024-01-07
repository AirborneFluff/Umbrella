import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { StockItem } from '../../../core/models/stock-item';
import { PaginatedResult } from '../../../core/utilities/pagination';
import { getPaginatedResult, getPaginationHeaders } from '../../../core/utilities/pagination-helper';
import { PagedSearchParams } from '../../../core/params/paged-search-params';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private baseUrl = environment.apiUrl + 'stockItems/';

  constructor(private http: HttpClient) { }

  public getByPartCode(partCode: string): Observable<StockItem> {
    return this.http.get<StockItem>(this.baseUrl + partCode);
  }

  public getPaginatedList(stockParams: PagedSearchParams): Observable<PaginatedResult<StockItem[]>> {
    let params = getPaginationHeaders(stockParams.pageNumber, stockParams.pageSize);
    if (stockParams.searchTerm) params = params.set('searchTerm', stockParams.searchTerm);
    return getPaginatedResult<StockItem[]>(this.baseUrl, params, this.http);
  }
}
