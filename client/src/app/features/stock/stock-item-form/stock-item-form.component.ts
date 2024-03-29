import { Component, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StockItem } from '../../../core/models/stock-item';
import { StockSupplySource } from '../../../core/models/stock-supply-source';
import { BehaviorSubject, distinctUntilChanged, Subscription } from 'rxjs';
import { StockService } from '../../../core/services/stock.service';

type FormControlName = keyof typeof StockItemFormComponent.prototype.form.controls;

@Component({
  selector: 'app-stock-item-form',
  templateUrl: './stock-item-form.component.html',
  styleUrls: ['./stock-item-form.component.scss']
})
export class StockItemFormComponent implements OnInit, OnDestroy {
  private subscriptions = new Subscription();
  private isPristine$ = new BehaviorSubject(true);
  @Output() isPristine = this.isPristine$.pipe(distinctUntilChanged());

  form = new FormGroup({
    id: new FormControl<string>({value: '', disabled: true}, {nonNullable: true}),
    partCode: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
    description: new FormControl<string>('', {validators: [Validators.required]}),
    category: new FormControl<string | null>(null),
    location: new FormControl<string | null>(null),
    supplySources: new FormControl<StockSupplySource[]>([], {nonNullable: true})
  })

  constructor(private stockApi: StockService) {}

  categoryOptions$ = this.stockApi.getCategories();

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe()
  }

  ngOnInit(): void {
    this.subscriptions.add(
      this.form.valueChanges.subscribe(() => this.isPristine$.next(this.form.pristine))
    )
  }

  get formValue(): StockItem {
    return this.form.getRawValue() as StockItem;
  }

  getControl(controlName: FormControlName) {
    return this.form.controls[controlName] as FormControl;
  }

  hasRequiredError(controlName: FormControlName) {
    const control = this.getControl(controlName);
    if (!control.dirty) return false;
    return control.hasError('required');
  }

  patchForm(item: StockItem) {
    this.form.patchValue(item);
    this.getControl('partCode').disable();
  }
}
