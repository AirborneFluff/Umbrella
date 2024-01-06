import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { OrbButtonComponent } from './orb-button/orb-button.component';
import { MatButtonModule } from "@angular/material/button";
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { IsVisibleDirective } from './directives/is-visible.directive';

@NgModule({
  declarations: [
    OrbButtonComponent,
    IsVisibleDirective
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatBottomSheetModule,
    MatListModule,
    MatPaginatorModule
  ],
  exports: [
    MatIconModule,
    OrbButtonComponent,
    MatBottomSheetModule,
    MatListModule,
    MatPaginatorModule,
    IsVisibleDirective
  ]
})
export class SharedModule { }
