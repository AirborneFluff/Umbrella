import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-stock-item-form',
  templateUrl: './stock-item-form.component.html',
  styleUrls: ['./stock-item-form.component.scss']
})
export class StockItemFormComponent {
  form = new FormGroup({
    partCode: new FormControl('', {validators: [Validators.required]}),
    description: new FormControl('', {validators: [Validators.required]}),
    location: new FormControl([])
  })

  constructor() {}

}
