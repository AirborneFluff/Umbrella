import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockHomeComponent } from './stock-home/stock-home.component';

const routes: Routes = [
  {
    path: '',
    component: StockHomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }
