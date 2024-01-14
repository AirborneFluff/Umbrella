import { Component, ViewChild } from '@angular/core';
import { StockItemFormComponent } from '../stock-item-form/stock-item-form.component';
import { StockService } from '../services/stock.service';

@Component({
  selector: 'app-new-stock-item',
  templateUrl: './new-stock-item.component.html',
  styleUrls: ['./new-stock-item.component.scss']
})
export class NewStockItemComponent {
  @ViewChild(StockItemFormComponent) formComponent!: StockItemFormComponent;

  constructor(private stockApi: StockService) {
  }

  save() {
    const item = this.formComponent.formValue;
    this.stockApi.addNew(item).subscribe({
      next: newItem => console.log(newItem)
    })
  }

}
