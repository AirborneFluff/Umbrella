import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { StockItemFormComponent } from '../stock-item-form/stock-item-form.component';
import { StockService } from '../../../core/services/stock.service';
import { StockItemIdStream } from '../../../core/streams/stock-item-id-stream';
import { notNullOrUndefined } from '../../../core/pipes/not-null';
import { finalize, shareReplay, switchMap, take } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ComponentCanDeactivate } from '../../../core/guards/prevent-unsaved-changes.guard';

@Component({
  selector: 'app-edit-stock-item',
  templateUrl: './edit-stock-item.component.html',
  styleUrls: ['./edit-stock-item.component.scss']
})
export class EditStockItemComponent implements AfterViewInit, ComponentCanDeactivate {
  @ViewChild(StockItemFormComponent, {static: false}) formComponent!: StockItemFormComponent;
  busy = false;
  formPristine = true;

  constructor(private stockApi: StockService,
              private id$: StockItemIdStream,
              private changeDetector: ChangeDetectorRef,
              private router: Router,
              private route: ActivatedRoute) {}

  stockItem$ = this.id$.pipe(
    take(1),
    notNullOrUndefined(),
    switchMap(id => this.stockApi.getById(id)),
    shareReplay(1)
  )

  canDeactivate() {
    return this.formPristine;
  }

  save() {
    const item = this.formComponent.formValue;
    this.busy = true;

    this.stockApi.update(item).pipe(
      finalize(() => this.busy = false)
    ).subscribe(() => {
      this.formPristine = true;
      this.router.navigate(['../'], { relativeTo: this.route })
    })
  }

  ngAfterViewInit(): void {
    this.stockItem$
      .subscribe(item => {
        this.changeDetector.detectChanges();
        this.formComponent.patchForm(item)
      });
  }
}
