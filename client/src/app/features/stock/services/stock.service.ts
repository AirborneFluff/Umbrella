import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { StockItem } from '../../../core/models/stock-item';
import { PaginatedResult, PaginationParams } from '../../../core/utilities/pagination';
import { getPaginatedResult, getPaginationHeaders } from '../../../core/utilities/pagination-helper';
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

  public getPaginatedList(pageParams: PaginationParams, additionalParams?: HttpParams): Observable<PaginatedResult<StockItem[]>> {
    let params = getPaginationHeaders(pageParams.pageNumber, pageParams.pageSize);

    if (additionalParams) {
      params = mergeParams(params, additionalParams);
    }

    return getPaginatedResult<StockItem[]>(this.baseUrl, params, this.http);
  }
}
