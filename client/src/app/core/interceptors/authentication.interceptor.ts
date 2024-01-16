import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor(private account: AccountService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((requestError: HttpErrorResponse) => {
        if (requestError?.status === 401) {
          this.account.logout();
          this.router.navigateByUrl('');
        }
        return throwError(() => requestError);
      })
    );
  }
}
