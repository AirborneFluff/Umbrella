import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import {
  distinctUntilChanged,
  filter,
  map,
  shareReplay,
  startWith, tap,
} from 'rxjs';

import { InjectableStream } from './injectable-stream';
import { RootFeatures } from '../definitions/root-features';

@Injectable({
  providedIn: 'root',
})
export class RootFeatureStream extends InjectableStream<string | undefined> {
  constructor(router: Router) {
    super(
      router.events.pipe(
        filter(event => event instanceof NavigationEnd),
        startWith(undefined),
        map(value => {
          if (!(value as NavigationEnd)?.urlAfterRedirects) return undefined;

          const rootValue = this.getRootValue((value as NavigationEnd).urlAfterRedirects);
          if (!this.isValidFeature(rootValue)) return undefined;

          return rootValue;
        }),
        distinctUntilChanged(),
        shareReplay(1)
      )
    );
  }

  private isValidFeature(value: string | undefined): value is keyof typeof RootFeatures {
    if (!value) return false;
    return Object.values(RootFeatures).includes(value as keyof typeof RootFeatures);
  }

  private getRootValue(url: string): string | undefined {
    const fragments = url.split('/');
    if (fragments.length <= 2) return undefined;

    return fragments[2];
  }
}
