import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RootFeatures } from './core/definitions/root-features';

const routes: Routes = [
  {
    path: RootFeatures.STOCK_ROOT,
    loadChildren: () => import('./features/stock/stock.module').then(m => m.StockModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
