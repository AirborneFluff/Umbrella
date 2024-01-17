import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormFieldComponent } from './form-field/form-field.component';
import { AppInputDirective } from './directives/app-input.directive';
import { AppInputGhostDirective } from './directives/app-input-ghost.directive';
import { FormLabelComponent } from './form-label/form-label.component';
import { FormErrorComponent } from './form-error/form-error.component';



@NgModule({
  declarations: [
    FormFieldComponent,
    AppInputDirective,
    AppInputGhostDirective,
    FormLabelComponent,
    FormErrorComponent,
  ],
  exports: [
    FormFieldComponent,
    AppInputDirective,
    AppInputGhostDirective,
    FormLabelComponent,
    FormErrorComponent,
  ],
  imports: [
    CommonModule,
  ]
})
export class AppFormsModule { }
