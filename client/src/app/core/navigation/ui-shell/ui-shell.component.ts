import { Component } from '@angular/core';
import { map } from "rxjs";
import { SideMenuLayouts } from "../side-menu-layouts";
import { BreakpointStream } from '../../streams/breakpoint-stream';
import { Breakpoints } from '../../definitions/breakpoints';

@Component({
  selector: 'app-ui-shell',
  templateUrl: './ui-shell.component.html',
  styleUrls: ['./ui-shell.component.scss']
})
export class UiShellComponent {
  constructor(private breakpoint$: BreakpointStream) {}

  sideMenuLayout$ = this.breakpoint$.pipe(
    map(result => {
      if (result.breakpoints[Breakpoints.xl]) return SideMenuLayouts.Regular;
      return SideMenuLayouts.Compact;
    })
  )

  showMobileView$ = this.breakpoint$.pipe(
    map(result => !result.matches)
  )
  protected readonly SideMenuLayouts = SideMenuLayouts;
}
