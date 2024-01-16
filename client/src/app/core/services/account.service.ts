import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { catchError, Observable, of, ReplaySubject, tap } from 'rxjs';
import { AppUser } from '../models/app-user';
import { HttpClient } from '@angular/common/http';
import { LoginParams } from '../models/login-params';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = environment.apiUrl;
  private currentUserSource$ = new ReplaySubject<AppUser | undefined>(1);
  public currentUser$ = this.currentUserSource$.asObservable();

  constructor(private http: HttpClient) {
    this.getUserDetails().subscribe(val => this.currentUserSource$.next(val));
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
