import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { take } from 'rxjs';

export const loggedInGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  let currentUser;
  accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
  console.log(currentUser)

  return !!currentUser;
};
