import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QueryFilterComponent } from './query-filter/query-filter.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { QueryFilterSheetComponent } from './query-filter-sheet/query-filter-sheet.component';

@NgModule({
  declarations: [
    QueryFilterComponent,
    QueryFilterSheetComponent
  ],
  exports: [
    QueryFilterComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ]
})
export class QueryFilterModule { }
