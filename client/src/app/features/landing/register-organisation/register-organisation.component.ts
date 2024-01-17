import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterOrganisationParams } from '../../../core/models/register-organisation-params';
import { OrganisationService } from '../../../core/services/organisation.service';
import { matchValues } from '../../../core/form-validators/match-values-validator';
import { finalize, Subscription } from 'rxjs';
import { passwordStrengthValidator } from '../../../core/form-validators/password-strength-validator';

type FormControlName = keyof typeof RegisterOrganisationComponent.prototype.form.controls;

@Component({
  selector: 'app-register-organisation',
  templateUrl: './register-organisation.component.html',
  styleUrls: ['./register-organisation.component.scss']
})
export class RegisterOrganisationComponent implements OnDestroy {
  busy = false;
  success: boolean | undefined;

  subscriptions = new Subscription();

  form = new FormGroup({
    organisationName: new FormControl<string>('', {
      validators: [Validators.required, Validators.minLength(3), Validators.maxLength(32)], nonNullable: true
    }),
    email: new FormControl<string>('', {
      validators: [Validators.required, Validators.email], nonNullable: true
    }),
    password: new FormControl<string>('', {
      validators: [Validators.required, Validators.minLength(6), Validators.maxLength(32), passwordStrengthValidator()], nonNullable: true
    }),
    confirmPassword: new FormControl<string>('', {
      validators: [Validators.required, matchValues('password')], nonNullable: true
    }),
  })

  constructor(private organisation: OrganisationService) {
    this.subscriptions.add(
      this.form.controls['password'].valueChanges.subscribe(() => {
        this.form.controls['confirmPassword'].updateValueAndValidity();
      })
    )
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  get formValue(): RegisterOrganisationParams {
    return this.form.getRawValue() as RegisterOrganisationParams;
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

  register() {
    this.busy = true;
    this.organisation.createOrganisation(this.formValue).pipe(
      finalize(() => this.busy = false)
    ).subscribe({
      next: () => this.success = true,
      error: e => console.log(e)
    })
  }
}
