import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { StockItem } from '../models/stock-item';
import { PaginatedResult, PaginationParams } from '../utilities/pagination';
import { getPaginatedResult, getPaginationHeaders } from '../utilities/pagination-helper';
import { mergeParams } from '../utilities/http-params-helper';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private baseUrl = environment.apiUrl + 'stockItems/';

  constructor(private http: HttpClient) { }

  public getById(id: string): Observable<StockItem> {
    return this.http.get<StockItem>(this.baseUrl + id);
  }

  public getPaginatedList(pageParams: PaginationParams, additionalParams?: HttpParams): Observable<PaginatedResult<StockItem[]>> {
    let params = getPaginationHeaders(pageParams.pageNumber, pageParams.pageSize);

    if (additionalParams) {
      params = mergeParams(params, additionalParams);
    }

    return getPaginatedResult<StockItem[]>(this.baseUrl, params, this.http);
  }

  public getCategories(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl + 'categories');
  }

  public addNew(item: Partial<StockItem>): Observable<StockItem> {
    return this.http.post<StockItem>(this.baseUrl, item)
  }

  public update(item: StockItem): Observable<StockItem> {
    const value: Omit<StockItem, 'id' | 'partCode'> = item;
    return this.http.put<StockItem>(this.baseUrl + item.id, value);
  }
}
