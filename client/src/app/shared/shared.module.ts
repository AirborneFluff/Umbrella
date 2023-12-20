import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { OrbButtonComponent } from './orb-button/orb-button.component';
import { MatButtonModule } from "@angular/material/button";

@NgModule({
  declarations: [
    OrbButtonComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
  ],
  exports: [
    MatIconModule,
    OrbButtonComponent
  ]
})
export class SharedModule { }
