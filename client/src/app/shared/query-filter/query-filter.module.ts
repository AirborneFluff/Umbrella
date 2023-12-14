import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QueryFilterComponent } from './query-filter/query-filter.component';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    QueryFilterComponent
  ],
  exports: [
    QueryFilterComponent
  ],
  imports: [
    CommonModule,
    MatIconModule
  ]
})
export class QueryFilterModule { }
