import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { map, take, tap } from 'rxjs';

export const loggedInGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  return inject(AccountService).currentUser$.pipe(
    take(1),
    map(user => {
      const canActivate = !!user;
      if (canActivate) return true;
      return router.parseUrl('');
    }));
};
