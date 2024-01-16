import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { map, take } from 'rxjs';

export const unauthenticatedGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  return inject(AccountService).currentUser$.pipe(
    take(1),
    map(user => {
      const canActivate = !user;
      if (canActivate) return true;
      return router.parseUrl('/app');
    }));
};
