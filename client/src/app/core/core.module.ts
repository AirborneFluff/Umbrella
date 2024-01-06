import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppFrameComponent } from "./components/app-frame/app-frame.component";
import { SharedModule } from "../shared/shared.module";
import { MenuEntryComponent } from "./components/menu-entry/menu-entry.component";
import { LayoutModule } from "@angular/cdk/layout";
import { UiShellComponent } from './navigation/ui-shell/ui-shell.component';
import { NavigationModule } from './navigation/navigation.module';
import { IsVisibleDirective } from './directives/is-visible.directive';

@NgModule({
  declarations: [
    AppFrameComponent,
    MenuEntryComponent,
    IsVisibleDirective
  ],
  imports: [
    CommonModule,
    SharedModule,
    LayoutModule,
    NavigationModule
  ],
  exports: [
    AppFrameComponent,
    UiShellComponent,
    IsVisibleDirective
  ]
})
export class CoreModule { }
