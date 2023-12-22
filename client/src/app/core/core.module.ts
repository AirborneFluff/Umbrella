import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppFrameComponent } from "./components/app-frame/app-frame.component";
import { SharedModule } from "../shared/shared.module";
import { MenuEntryComponent } from "./components/menu-entry/menu-entry.component";
import { SideMenuComponent } from "./navigation/side-menu/side-menu.component";
import { BottomMenuComponent } from "./navigation/bottom-menu/bottom-menu.component";
import { UiShellComponent } from './navigation/ui-shell/ui-shell.component';
import { LayoutModule } from "@angular/cdk/layout";
import { BottomMenuButtonComponent } from './navigation/bottom-menu-button/bottom-menu-button.component';
import { SideMenuButtonComponent } from './navigation/side-menu-button/side-menu-button.component';

@NgModule({
  declarations: [
    AppFrameComponent,
    MenuEntryComponent,
    SideMenuComponent,
    BottomMenuComponent,
    UiShellComponent,
    BottomMenuButtonComponent,
    SideMenuButtonComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    LayoutModule
  ],
  exports: [
    AppFrameComponent,
    UiShellComponent
  ]
})
export class CoreModule { }
