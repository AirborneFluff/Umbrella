import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordStrengthValidator(): ValidatorFn {
  return (control: AbstractControl) : ValidationErrors | null => {
    const value = control.value;

    if (!value) {
      return null;
    }

    let validationResults: any = {};

    if (!/[A-Z]+/.test(value)) validationResults.hasUpperCase = true;
    if (!/[a-z]+/.test(value)) validationResults.hasLowerCase = true;
    if (!/[0-9]+/.test(value)) validationResults.hasNumeric = true;
    if (!/[^a-zA-Z\d\s:]/.test(value)) validationResults.hasNonAlphaNumeric = true;

    const hasAnyValues: boolean = Object.values(validationResults).some(value => value === true);
    return !hasAnyValues ? null : { validationResults: true };
  }
}
