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

  handleError(error: HttpErrorResponse) {
    this.snackBar.open(this.getSnackbarMessage(error), 'Dismiss', {
      duration: 10000
    });

    switch (error?.status) {
      case 401:
        this.handleUnauthorized();
        break;
    }
  }

  handleUnauthorized() {
    const fragments = this.router.url.split('/');
    if (fragments[1] == 'app') {
      this.account.logout();
      this.router.navigateByUrl('');
    }
  }

  getSnackbarMessage(error: HttpErrorResponse): string {
    return `There was a problem. Error - ${error.status}: ${error.statusText}`
  }
}
