import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-side-menu-sheet',
  templateUrl: './side-menu-sheet.component.html',
  styleUrls: ['./side-menu-sheet.component.scss']
})
export class SideMenuSheetComponent {
  layout!: string;

  constructor(@Inject(MAT_BOTTOM_SHEET_DATA) public data: any) {
    this.layout = data;
  }

}
