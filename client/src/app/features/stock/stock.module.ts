import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StockRoutingModule } from './stock-routing.module';
import { StockHomeComponent } from './stock-home/stock-home.component';
import { StockListComponent } from './stock-list/stock-list.component';
import { SharedModule } from '../../shared/shared.module';
import { StockItemFormComponent } from './stock-item-form/stock-item-form.component';


@NgModule({
  declarations: [
    StockHomeComponent,
    StockListComponent,
    StockItemFormComponent
  ],
  imports: [
    CommonModule,
    StockRoutingModule,
    SharedModule
  ]
})
export class StockModule { }
