import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { StockItemFormComponent } from '../stock-item-form/stock-item-form.component';
import { StockService } from '../services/stock.service';
import { StockItemIdStream } from '../../../core/streams/stock-item-id-stream';
import { notNullOrUndefined } from '../../../core/pipes/not-null';
import { shareReplay, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-edit-stock-item',
  templateUrl: './edit-stock-item.component.html',
  styleUrls: ['./edit-stock-item.component.scss']
})
export class EditStockItemComponent implements AfterViewInit {
  @ViewChild(StockItemFormComponent, {static: false}) formComponent!: StockItemFormComponent;

  constructor(private stockApi: StockService, private id$: StockItemIdStream, private changeDetector: ChangeDetectorRef) {
  }

  stockItem$ = this.id$.pipe(
    notNullOrUndefined(),
    switchMap(id => this.stockApi.getById(id)),
    shareReplay(1)
  )

  save() {
    const item = this.formComponent.formValue;
    this.stockApi.update(item).subscribe(val => {
      this.formComponent.patchForm(item)
    })
  }

  ngAfterViewInit(): void {
    this.stockItem$
      .pipe(take(1))
      .subscribe(item => {
        this.changeDetector.detectChanges();
        this.formComponent.patchForm(item)
      });
  }

}
