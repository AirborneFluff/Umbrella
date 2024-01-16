import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RootFeatures } from './core/definitions/root-features';
import { loggedInGuard } from './core/guards/logged-in.guard';

const routes: Routes = [
  {
    path: RootFeatures.STOCK_ROOT,
    canActivate: [loggedInGuard],
    loadChildren: () => import('./features/stock/stock.module').then(m => m.StockModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
