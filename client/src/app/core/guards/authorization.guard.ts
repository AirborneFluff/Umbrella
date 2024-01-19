import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { map, take, tap } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

export function authorizationGuard(permissionRequirement: number): CanActivateFn {
  return () => {
    const accountService = inject(AccountService);
    const snackBar = inject(MatSnackBar);

    return accountService.currentUser$.pipe(
      take(1),
      map(user => {
        if (!user) return false;
        return (Number.parseInt(user.permissions) & permissionRequirement) > 0;
      }),
      tap(access => {
        if (access) return;
        snackBar.open("You don't have the permissions to access that route", 'Dismiss', {
          duration: 5000
        });
      }));
  }
}
