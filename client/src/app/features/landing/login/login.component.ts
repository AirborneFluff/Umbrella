import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginParams } from '../../../core/models/login-params';
import { AccountService } from '../../../core/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  busy = false;
  protected form = new FormGroup({
    email: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
    password: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
  })

  constructor(private account: AccountService, private router: Router) {}

  get formValue(): LoginParams {
    return this.form.getRawValue() as LoginParams;
  }

  login() {
    this.busy = true;
    this.account.login(this.formValue).subscribe({
      next: () => this.router.navigateByUrl('/app'),
      error: e => console.log(e)
    })
  }

}
