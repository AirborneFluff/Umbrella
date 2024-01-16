import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RootFeatures } from './core/definitions/root-features';
import { loggedInGuard } from './core/guards/logged-in.guard';
import { unauthenticatedGuard } from './core/guards/unauthenticated.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [unauthenticatedGuard],
    loadChildren: () => import('./features/landing/landing.module').then(m => m.LandingModule)
  },
  {
    path: 'app',
    canActivate: [loggedInGuard],
    children: [
      {
        path: RootFeatures.STOCK_ROOT,
        loadChildren: () => import('./features/stock/stock.module').then(m => m.StockModule)
      },
      {
        path: '**',
        redirectTo: RootFeatures.STOCK_ROOT
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
