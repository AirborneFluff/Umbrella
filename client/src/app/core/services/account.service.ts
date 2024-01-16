import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import {
  catchError,
  concat,
  distinctUntilChanged,
  Observable,
  of,
  ReplaySubject,
  shareReplay,
  take,
  tap
} from 'rxjs';
import { AppUser } from '../models/app-user';
import { HttpClient } from '@angular/common/http';
import { LoginParams } from '../models/login-params';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = environment.apiUrl;
  private currentUserSource$ = new ReplaySubject<AppUser | undefined>(1);
  public currentUser$ = concat(
    this.getUserDetails().pipe(take(1)),
    this.currentUserSource$
  ).pipe(
    distinctUntilChanged(),
    shareReplay(1))

  constructor(private http: HttpClient) {
  }

  private getUserDetails(): Observable<AppUser | undefined> {
    return this.http.get<AppUser>(this.baseUrl + "Account").pipe(
      catchError(e => {
        if (e.status == 401) return of(undefined);
        throw e;
      })
    )
  }

  public login(login: LoginParams) {
    return this.http.post<AppUser>(this.baseUrl + "Account/login", login).pipe(
      tap(user => this.currentUserSource$.next(user))
    );
  }

  public logout() {
    return this.http.post(this.baseUrl + "Account/logout", {}).pipe(
      tap(() => this.currentUserSource$.next(undefined))
    )
  }
}
