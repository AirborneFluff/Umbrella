import { Component, ViewChild } from '@angular/core';
import { StockItemFormComponent } from '../stock-item-form/stock-item-form.component';
import { StockService } from '../../../core/services/stock.service';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-new-stock-item',
  templateUrl: './new-stock-item.component.html',
  styleUrls: ['./new-stock-item.component.scss']
})
export class NewStockItemComponent {
  busy = false;
  @ViewChild(StockItemFormComponent) formComponent!: StockItemFormComponent;

  constructor(private stockApi: StockService,
              private router: Router,
              private route: ActivatedRoute) {}

  save() {
    const item = this.formComponent.formValue;
    this.busy = true;

    this.stockApi.addNew(item).pipe(
      finalize(() => this.busy = false)
    ).subscribe(() => {
      this.router.navigate(['../'], { relativeTo: this.route })
    })
  }

}
