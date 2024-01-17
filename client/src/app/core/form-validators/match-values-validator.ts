import { AbstractControl, ValidatorFn } from '@angular/forms';

export function matchValues(matchTo: string): ValidatorFn {
  return(control: AbstractControl) => {
    const matchedControl = control?.parent?.get(matchTo);
    return control?.value === matchedControl?.value ? null : { isMatching: true }
  }
}
