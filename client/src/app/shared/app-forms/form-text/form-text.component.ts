import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { BehaviorSubject, finalize, Observable, take } from 'rxjs';

@Component({
  selector: 'app-form-text',
  templateUrl: './form-text.component.html',
  styleUrls: ['./form-text.component.scss']
})
export class FormTextComponent implements ControlValueAccessor {
  @Input() placeholder!: string;
  @Input() options: string[] | null = null;
  @Input() optionsSource: Observable<string[]> | undefined;
  @Input() type: 'password' | undefined;

  constructor(@Self() public ngControl: NgControl) {
    if (this.ngControl) {
      ngControl.valueAccessor = this;
    }
  }

  optionsLoading$ = new BehaviorSubject(false);

  getOptionsFromSource() {
    if (!this.optionsSource) return;
    if (this.options) return;
    this.optionsLoading$.next(true);

    this.optionsSource.pipe(
      take(1),
      finalize(() => this.optionsLoading$.next(false))
    ).subscribe(options => {
      this.options = options;
    })
  }

  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }

  get valid(): boolean {
    return this.control.valid || this.control.pristine
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

  writeValue(obj: any): void {
  }

}
