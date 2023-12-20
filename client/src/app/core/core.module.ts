import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppFrameComponent } from "./components/app-frame/app-frame.component";
import { SharedModule } from "../shared/shared.module";
import { MenuEntryComponent } from "./components/menu-entry/menu-entry.component";

@NgModule({
  declarations: [
    AppFrameComponent,
    MenuEntryComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    AppFrameComponent
  ]
})
export class CoreModule { }
