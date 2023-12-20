import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from "../../shared/shared.module";
import { SideMenuComponent } from './side-menu/side-menu.component';
import { BottomMenuComponent } from './bottom-menu/bottom-menu.component';



@NgModule({
  declarations: [
    SideMenuComponent,
    BottomMenuComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class NavigationModule { }
