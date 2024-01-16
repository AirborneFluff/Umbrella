import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterOrganisationParams } from '../../../core/models/register-organisation-params';
import { OrganisationService } from '../../../core/services/organisation.service';

@Component({
  selector: 'app-register-organisation',
  templateUrl: './register-organisation.component.html',
  styleUrls: ['./register-organisation.component.scss']
})
export class RegisterOrganisationComponent {
  busy = false;
  success: boolean | undefined;

  protected form = new FormGroup({
    organisationName: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
    email: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
    password: new FormControl<string>('', {validators: [Validators.required], nonNullable: true}),
  })

  constructor(private organisation: OrganisationService) {}

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
