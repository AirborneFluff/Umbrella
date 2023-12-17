import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrameComponent } from './frame/frame.component';
import { SharedModule } from '../../shared/shared.module';



@NgModule({
  exports: [
    FrameComponent
  ],
  declarations: [
    FrameComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class AppFrameModule { }
