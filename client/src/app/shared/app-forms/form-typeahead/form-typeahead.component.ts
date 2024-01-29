import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { combineLatest, map, Observable, shareReplay, startWith } from 'rxjs';

@Component({
  selector: 'app-form-typeahead',
  templateUrl: './form-typeahead.component.html',
  styleUrls: ['./form-typeahead.component.scss']
})
export class FormTypeaheadComponent implements ControlValueAccessor, OnInit {
  @Input() placeholder!: string;
  @Input() type: 'password' | undefined;
  @Input()
  set optionSource(value: Observable<string[]>) {
    this.options$ = value.pipe(
      shareReplay(1)
    )
  }

  protected options$!: Observable<string[]>;
  protected filteredOptions$!: Observable<string[]>;

  constructor(@Self() public ngControl: NgControl) {
    if (this.ngControl) {
      ngControl.valueAccessor = this;
    }
  }

  ngOnInit(): void {
    this.filteredOptions$ = combineLatest([
      this.options$,
      this.control.valueChanges.pipe(startWith(''))
    ]).pipe(
      map(([options, inputValue]) => {
        if (!inputValue) return options;
        return options.filter(opt => opt.toLowerCase().includes(inputValue.toLowerCase()))
      }),
      shareReplay(1)
    )
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
