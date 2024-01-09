import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { FilterDefinition } from '../filter-definition';

@Component({
  selector: 'app-query-filter-sheet',
  templateUrl: './query-filter-sheet.component.html',
  styleUrls: ['./query-filter-sheet.component.scss']
})
export class QueryFilterSheetComponent {
  constructor(@Inject(MAT_BOTTOM_SHEET_DATA) public entityName: FilterDefinition, private bottomSheet: MatBottomSheetRef) {
  }

  close(event: any) {
    this.bottomSheet.dismiss(event);
  }
}
