import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrameComponent } from './frame/frame.component';
import { SharedModule } from '../../shared/shared.module';
import { MenuEntryComponent } from './menu-entry/menu-entry.component';



@NgModule({
  exports: [
    FrameComponent
  ],
  declarations: [
    FrameComponent,
    MenuEntryComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class AppFrameModule { }
