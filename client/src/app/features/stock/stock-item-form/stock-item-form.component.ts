import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StockItem } from '../../../core/models/stock-item';
import { StockSupplySource } from '../../../core/models/stock-supply-source';

@Component({
  selector: 'app-stock-item-form',
  templateUrl: './stock-item-form.component.html',
  styleUrls: ['./stock-item-form.component.scss']
})
export class StockItemFormComponent {
  protected form = new FormGroup({
    id: new FormControl<string>({value: '', disabled: true}, {nonNullable: true}),
    partCode: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
    description: new FormControl<string>('', {validators: [Validators.required]}),
    category: new FormControl<string>(''),
    location: new FormControl<string>(''),
    supplySources: new FormControl<StockSupplySource[]>([], {nonNullable: true})
  })

  constructor() {}

  get formValue(): StockItem {
    return this.form.getRawValue() as StockItem;
  }

  patchForm(item: StockItem) {
    this.form.patchValue(item);
  }
}
