import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterOrganisationParams } from '../../../core/models/register-organisation-params';
import { OrganisationService } from '../../../core/services/organisation.service';
import { matchValues } from '../../../core/form-validators/match-values-validator';
import { Subscription } from 'rxjs';
import { passwordStrengthValidator } from '../../../core/form-validators/password-strength-validator';

@Component({
  selector: 'app-register-organisation',
  templateUrl: './register-organisation.component.html',
  styleUrls: ['./register-organisation.component.scss']
})
export class RegisterOrganisationComponent implements OnDestroy {
  busy = false;
  success: boolean | undefined;

  subscriptions = new Subscription();

  protected form = new FormGroup({
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


  register() {
    this.busy = true;
    this.organisation.createOrganisation(this.formValue).subscribe({
      next: () => this.success = true,
      error: e => console.log(e)
    })
  }
}
