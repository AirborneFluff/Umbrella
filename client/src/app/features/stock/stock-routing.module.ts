import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockListComponent } from './stock-list/stock-list.component';
import { StockItemFormComponent } from './stock-item-form/stock-item-form.component';
import { NewStockItemComponent } from './new-stock-item/new-stock-item.component';

const routes: Routes = [
  {
    path: '',
    component: StockListComponent,
  },
  {
    path: 'new',
    component: NewStockItemComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }
