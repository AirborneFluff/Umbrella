import { Component } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { SideMenuLayouts } from '../side-menu-layouts';
import { SideMenuSheetComponent } from '../side-menu-sheet/side-menu-sheet.component';

@Component({
  selector: 'app-bottom-menu',
  templateUrl: './bottom-menu.component.html',
  styleUrls: ['./bottom-menu.component.scss']
})
export class BottomMenuComponent {

  constructor(private bottomSheet: MatBottomSheet) {
  }

  openSideMenu() {
    //todo Use Material side nav instead?
    this.bottomSheet.open(SideMenuSheetComponent, {
      direction: 'ltr',
      panelClass: 'w-full',
      data: SideMenuLayouts.Full
    });
  }
}
