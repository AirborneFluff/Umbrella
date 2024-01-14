import { Component, ViewChild } from '@angular/core';
import { StockItemFormComponent } from '../stock-item-form/stock-item-form.component';
import { StockService } from '../services/stock.service';
import { StockItemIdStream } from '../../../core/streams/stock-item-id-stream';
import { notNullOrUndefined } from '../../../core/pipes/not-null';
import { Subscription, switchMap } from 'rxjs';

@Component({
  selector: 'app-edit-stock-item',
  templateUrl: './edit-stock-item.component.html',
  styleUrls: ['./edit-stock-item.component.scss']
})
export class EditStockItemComponent {
  @ViewChild(StockItemFormComponent) formComponent!: StockItemFormComponent;
  private subscriptions = new Subscription();

  constructor(private stockApi: StockService, private id$: StockItemIdStream) {
    this.subscriptions.add(
      this.stockItem$.subscribe(item => this.formComponent.patchForm(item)));
  }

  stockItem$ = this.id$.pipe(
    notNullOrUndefined(),
    switchMap(id => this.stockApi.getByPartCode(id))
  )

  save() {
    const item = this.formComponent.formValue;
    // this.stockApi.addNew(item).subscribe({
    //   next: newItem => console.log(newItem)
    // })
  }

}
