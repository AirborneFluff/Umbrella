import { InjectableStream } from './injectable-stream';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Injectable } from '@angular/core';
import { Breakpoints } from '../definitions/breakpoints';

@Injectable({
  providedIn: 'root',
})
export class BreakpointStream extends InjectableStream<BreakpointState> {
  constructor(private breakpoint: BreakpointObserver) {
    super(breakpoint.observe([
      Breakpoints.md,
      Breakpoints.lg,
      Breakpoints.xl,
    ]));
  }
}
