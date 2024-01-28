import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { OrbButtonComponent } from './orb-button/orb-button.component';
import { MatButtonModule } from "@angular/material/button";
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { IsVisibleDirective } from './directives/is-visible.directive';
import { OrbSearchComponent } from './orb-search/orb-search.component';
import { QueryFilterModule } from './query-filter/query-filter.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormFieldComponent } from './app-forms/form-field/form-field.component';
import { FormLabelComponent } from './app-forms/form-label/form-label.component';
import { FormErrorComponent } from './app-forms/form-error/form-error.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';
import { RepeatDirective } from './directives/repeat.directive';
import { FormTextComponent } from './app-forms/form-text/form-text.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

@NgModule({
  declarations: [
    OrbButtonComponent,
    IsVisibleDirective,
    OrbSearchComponent,
    FormFieldComponent,
    FormLabelComponent,
    FormErrorComponent,
    RepeatDirective,
    FormTextComponent,
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatBottomSheetModule,
    MatListModule,
    MatPaginatorModule,
    QueryFilterModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    NgxSkeletonLoaderModule,
    MatAutocompleteModule
  ],
  exports: [
    MatIconModule,
    OrbButtonComponent,
    MatBottomSheetModule,
    MatListModule,
    MatPaginatorModule,
    IsVisibleDirective,
    OrbSearchComponent,
    QueryFilterModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    FormFieldComponent,
    FormLabelComponent,
    FormErrorComponent,
    MatSnackBarModule,
    NgxSkeletonLoaderModule,
    RepeatDirective,
    FormTextComponent,
    MatAutocompleteModule
  ],
})
export class SharedModule { }
