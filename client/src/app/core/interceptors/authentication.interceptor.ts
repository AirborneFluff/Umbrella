import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor(private account: AccountService, private router: Router, private snackBar: MatSnackBar) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((requestError: HttpErrorResponse) => {
        this.handleError(requestError);
        return throwError(() => requestError);
      })
    );
  }

  private handleError(error: HttpErrorResponse) {
    switch (error?.status) {
      case 401:
        this.handleUnauthorized();
        break;
    }

    this.postSnackbar(error);
  }

  private handleUnauthorized() {
    const fragments = this.router.url.split('/');
    if (fragments[1] == 'app') {
      this.account.logout('login');
      return;
    }
  }

  private postSnackbar(error: HttpErrorResponse) {
    this.snackBar.open(`There was a problem. Error - ${error.status}: ${error.statusText}`, 'Dismiss', {
      duration: 10000
    });
  }
}
