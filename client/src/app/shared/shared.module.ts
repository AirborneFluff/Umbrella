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
import { AppInputDirective } from './app-forms/directives/app-input.directive';
import { AppInputGhostDirective } from './app-forms/directives/app-input-ghost.directive';
import { FormLabelComponent } from './app-forms/form-label/form-label.component';
import { FormErrorComponent } from './app-forms/form-error/form-error.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';
import { RepeatDirective } from './directives/repeat.directive';

@NgModule({
  declarations: [
    OrbButtonComponent,
    IsVisibleDirective,
    OrbSearchComponent,
    FormFieldComponent,
    AppInputDirective,
    AppInputGhostDirective,
    FormLabelComponent,
    FormErrorComponent,
    RepeatDirective,
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
    NgxSkeletonLoaderModule
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
    AppInputDirective,
    AppInputGhostDirective,
    FormLabelComponent,
    FormErrorComponent,
    MatSnackBarModule,
    NgxSkeletonLoaderModule,
    RepeatDirective
  ],
})
export class SharedModule { }
