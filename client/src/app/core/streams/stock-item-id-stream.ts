import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import {
  distinctUntilChanged,
  filter,
  map,
  shareReplay,
  startWith,
} from 'rxjs';

import { getRouteParamsFromSnapshot } from '../utilities/get-route-params';
import { InjectableStream } from './injectable-stream';
import { NavigationParams } from '../config/navigation_params';

@Injectable({
  providedIn: 'root',
})
export class StockItemIdStream extends InjectableStream<string | undefined> {
  constructor(router: Router) {
    super(
      router.events.pipe(
        filter(event => event instanceof NavigationEnd),
        startWith(undefined),
        map(() =>
          getRouteParamsFromSnapshot<string>(
            router.routerState.root.snapshot,
            NavigationParams.STOCK_ITEM_ID
          )
        ),
        distinctUntilChanged(),
        shareReplay(1)
      )
    );
  }
}
