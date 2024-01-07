import { HttpClient, HttpParams } from '@angular/common/http';
import { filter, map } from 'rxjs';
import { PaginatedResult } from './pagination';

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
  const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
  return http.get<T>(url, { observe: "response", params }).pipe(
    filter(result => result.body != null),
    map(response => {
      paginatedResult.result = response.body!;
      const paginationHeader = response.headers.get("Pagination");
      if (paginationHeader !== null) {
        paginatedResult.pagination = JSON.parse(paginationHeader);
      }
      return paginatedResult;
    })
  );
}

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
  let params = new HttpParams;
  params = params.append("pageNumber", pageNumber.toString());
  params = params.append("pageSize", pageSize.toString());

  return params;
}
