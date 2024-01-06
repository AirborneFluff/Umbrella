import { Component } from '@angular/core';
import { BreakpointObserver } from "@angular/cdk/layout";
import { map } from "rxjs";
import { Breakpoints } from "../breakpoints";
import { SideMenuLayouts } from "../side-menu-layouts";

@Component({
  selector: 'app-ui-shell',
  templateUrl: './ui-shell.component.html',
  styleUrls: ['./ui-shell.component.scss']
})
export class UiShellComponent {
  constructor(private breakpoint: BreakpointObserver) {}

  private breakpoints$ = this.breakpoint.observe([
    Breakpoints.md,
    Breakpoints.lg,
    Breakpoints.xl,
  ])

  sideMenuLayout$ = this.breakpoints$.pipe(
    map(result => {
      if (result.breakpoints[Breakpoints.xl]) return SideMenuLayouts.Regular;
      return SideMenuLayouts.Compact;
    })
  )

  showMobileView$ = this.breakpoints$.pipe(
    map(result => !result.matches)
  )
  protected readonly SideMenuLayouts = SideMenuLayouts;
}
