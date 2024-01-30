import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { combineLatest, map, Observable, shareReplay, startWith, take } from 'rxjs';

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
  @Input() requireSelection: boolean = false;
  @ViewChild('input') inputElement!: ElementRef;

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

  checkInputValid() {
    if (!this.requireSelection) return;

    const value = this.inputElement.nativeElement.value;
    this.options$.pipe(take(1)).subscribe(options => {
      const selectionValid = options.includes(value);

      if (selectionValid) return;
      this.control.setValue(null);
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
