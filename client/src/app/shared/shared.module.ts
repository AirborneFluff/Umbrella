import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { OrbButtonComponent } from './orb-button/orb-button.component';
import { MatButtonModule } from "@angular/material/button";
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { IsVisibleDirective } from './directives/is-visible.directive';
import { OrbSearchComponent } from './orb-search/orb-search.component';
import { QueryFilterModule } from './query-filter/query-filter.module';
import { AppFormsModule } from './app-forms/app-forms.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    OrbButtonComponent,
    IsVisibleDirective,
    OrbSearchComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatBottomSheetModule,
    MatListModule,
    MatPaginatorModule,
    QueryFilterModule,
    AppFormsModule,
    ReactiveFormsModule
  ],
  exports: [
    MatIconModule,
    OrbButtonComponent,
    MatBottomSheetModule,
    MatListModule,
    MatPaginatorModule,
    IsVisibleDirective,
    OrbSearchComponent,
    QueryFilterModule,
    AppFormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
