import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginParams } from '../../../core/models/login-params';
import { AccountService } from '../../../core/services/account.service';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { passwordStrengthValidator } from '../../../core/form-validators/password-strength-validator';

type FormControlName = keyof typeof LoginComponent.prototype.form.controls;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  busy = false;
  error: string | undefined;

  form = new FormGroup({
    email: new FormControl<string>('', {
      validators: [Validators.required, Validators.email], nonNullable: true
    }),
    password: new FormControl<string>('', {
      validators: [Validators.required, Validators.minLength(6), Validators.maxLength(32), passwordStrengthValidator()], nonNullable: true
    }),
  })

  constructor(private account: AccountService, private router: Router) {}

  get formValue(): LoginParams {
    return this.form.getRawValue() as LoginParams;
  }

  getControl(controlName: FormControlName) {
    return this.form.controls[controlName] as FormControl;
  }

  hasRequiredError(controlName: FormControlName) {
    const control = this.getControl(controlName);
    if (!control.dirty) return false;
    return control.hasError('required');
  }

  hasErrors(controlName: FormControlName) {
    const control = this.getControl(controlName);
    if (!control.dirty) return false;
    return !!control.errors;
  }

  login() {
    this.busy = true;
    this.account.login(this.formValue).pipe(
      finalize(() => this.busy = false)
    ).subscribe({
      next: () => this.router.navigateByUrl('/app'),
      error: e => this.error = e.error
    })
  }
}
