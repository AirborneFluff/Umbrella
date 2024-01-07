import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { StockItem } from '../../../core/models/stock-item';
import { PaginatedResult, PaginationParams } from '../../../core/utilities/pagination';
import { getPaginatedResult, getPaginationHeaders } from '../../../core/utilities/pagination-helper';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private baseUrl = environment.apiUrl + 'stockItems/';

  constructor(private http: HttpClient) { }

  public getByPartCode(partCode: string): Observable<StockItem> {
    return this.http.get<StockItem>(this.baseUrl + partCode);
  }

  public getPaginatedList(stockParams: PaginationParams): Observable<PaginatedResult<StockItem[]>> {
    let params = getPaginationHeaders(stockParams.pageNumber, stockParams.pageSize);
    return getPaginatedResult<StockItem[]>(this.baseUrl, params, this.http);
  }
}
