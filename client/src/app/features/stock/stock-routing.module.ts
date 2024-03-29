import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockListComponent } from './stock-list/stock-list.component';
import { NewStockItemComponent } from './new-stock-item/new-stock-item.component';
import { NavigationParams } from '../../core/config/navigation_params';
import { EditStockItemComponent } from './edit-stock-item/edit-stock-item.component';
import { authorizationGuard } from '../../core/guards/authorization.guard';
import { UserPermissions } from '../../core/definitions/user-permissions';
import { preventUnsavedChangesGuard } from '../../core/guards/prevent-unsaved-changes.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [authorizationGuard(UserPermissions.ReadStockItems)],
    component: StockListComponent,
  },
  {
    path: 'new',
    canActivate: [authorizationGuard(UserPermissions.CreateStockItems)],
    canDeactivate: [preventUnsavedChangesGuard],
    component: NewStockItemComponent
  },
  {
    path: `:${NavigationParams.STOCK_ITEM_ID}`,
    canActivate: [authorizationGuard(UserPermissions.EditStockItems)],
    canDeactivate: [preventUnsavedChangesGuard],
    component: EditStockItemComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }
