import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockListComponent } from './stock-list/stock-list.component';
import { StockItemFormComponent } from './stock-item-form/stock-item-form.component';
import { NewStockItemComponent } from './new-stock-item/new-stock-item.component';
import { NavigationParams } from '../../core/config/navigation_params';
import { EditStockItemComponent } from './edit-stock-item/edit-stock-item.component';

const routes: Routes = [
  {
    path: '',
    component: StockListComponent,
  },
  {
    path: 'new',
    component: NewStockItemComponent
  },
  {
    path: `:${NavigationParams.STOCK_ITEM_ID}`,
    component: EditStockItemComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }
