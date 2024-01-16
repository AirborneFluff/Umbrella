import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginParams } from '../../../core/models/login-params';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  protected form = new FormGroup({
    email: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
    password: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
  })

  constructor() {}

  get formValue(): LoginParams {
    return this.form.getRawValue() as LoginParams;
  }
}
