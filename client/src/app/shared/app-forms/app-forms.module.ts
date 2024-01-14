import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormFieldComponent } from './form-field/form-field.component';
import { AppInputDirective } from './directives/app-input.directive';
import { AppInputGhostDirective } from './directives/app-input-ghost.directive';



@NgModule({
  declarations: [
    FormFieldComponent,
    AppInputDirective,
    AppInputGhostDirective
  ],
  exports: [
    FormFieldComponent,
    AppInputDirective,
    AppInputGhostDirective
  ],
  imports: [
    CommonModule
  ]
})
export class AppFormsModule { }
