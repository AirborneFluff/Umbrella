import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { BottomMenuComponent } from './bottom-menu/bottom-menu.component';
import { UiShellComponent } from './ui-shell/ui-shell.component';
import { BottomMenuButtonComponent } from './bottom-menu-button/bottom-menu-button.component';
import { SideMenuButtonComponent } from './side-menu-button/side-menu-button.component';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { SharedModule } from '../../shared/shared.module';
import { RouterLink } from '@angular/router';

@NgModule({
  declarations: [
    SideMenuComponent,
    BottomMenuComponent,
    UiShellComponent,
    BottomMenuButtonComponent,
    SideMenuButtonComponent,
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatSidenavModule,
    SharedModule,
    RouterLink,
  ],
  exports: [
    UiShellComponent
  ]
})
export class NavigationModule { }
