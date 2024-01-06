import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { StockItem } from '../../../core/models/stock-item';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  baseUrl = environment.apiUrl + 'stockItems/';

  constructor(private http: HttpClient) { }

  public getByPartCode(partCode: string): Observable<StockItem> {
    return this.http.get<StockItem>(this.baseUrl + partCode);
  }
}
