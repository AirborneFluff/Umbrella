import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppFrameModule } from './app-frame/app-frame.module';



@NgModule({
  exports: [
    AppFrameModule
  ],
  declarations: [],
  imports: [
    CommonModule,
    AppFrameModule
  ]
})
export class FeaturesModule { }
