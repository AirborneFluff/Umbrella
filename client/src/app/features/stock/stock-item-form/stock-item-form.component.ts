import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StockItem } from '../../../core/models/stock-item';

@Component({
  selector: 'app-stock-item-form',
  templateUrl: './stock-item-form.component.html',
  styleUrls: ['./stock-item-form.component.scss']
})
export class StockItemFormComponent {
  protected form = new FormGroup({
    partCode: new FormControl<string>('', {validators: [Validators.required]}),
    description: new FormControl<string>('', {validators: [Validators.required]}),
    location: new FormControl<string>('',)
  })

  constructor() {}

  get formValue(): StockItem {
    return this.form.value as StockItem;
  }

  patchForm(item: StockItem) {
    this.form.patchValue(item);
  }
}
