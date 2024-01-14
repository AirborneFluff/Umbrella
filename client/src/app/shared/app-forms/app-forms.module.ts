import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormFieldComponent } from './form-field/form-field.component';
import { AppInputDirective } from './directives/app-input.directive';



@NgModule({
  declarations: [
    FormFieldComponent,
    AppInputDirective
  ],
  exports: [
    FormFieldComponent,
    AppInputDirective
  ],
  imports: [
    CommonModule
  ]
})
export class AppFormsModule { }
