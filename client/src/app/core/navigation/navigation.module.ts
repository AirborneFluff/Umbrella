import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { BottomMenuComponent } from './bottom-menu/bottom-menu.component';
import { UiShellComponent } from './ui-shell/ui-shell.component';
import { BottomMenuButtonComponent } from './bottom-menu-button/bottom-menu-button.component';
import { SideMenuButtonComponent } from './side-menu-button/side-menu-button.component';
import { SideMenuSheetComponent } from './side-menu-sheet/side-menu-sheet.component';
import { SideMenuDrawerComponent } from './side-menu-drawer/side-menu-drawer.component';



@NgModule({
  declarations: [
    SideMenuComponent,
    BottomMenuComponent,
    UiShellComponent,
    BottomMenuButtonComponent,
    SideMenuButtonComponent,
    SideMenuSheetComponent,
    SideMenuDrawerComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    UiShellComponent
  ]
})
export class NavigationModule { }
