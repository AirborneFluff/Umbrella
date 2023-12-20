import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppFrameComponent } from "./components/frame/app-frame.component";
import { SharedModule } from "../shared/shared.module";



@NgModule({
  declarations: [
    AppFrameComponent
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
