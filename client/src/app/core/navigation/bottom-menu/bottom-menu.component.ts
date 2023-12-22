import { Component } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { SideMenuComponent } from '../side-menu/side-menu.component';
import { SideMenuLayouts } from '../side-menu-layouts';

@Component({
  selector: 'app-bottom-menu',
  templateUrl: './bottom-menu.component.html',
  styleUrls: ['./bottom-menu.component.scss']
})
export class BottomMenuComponent {

  constructor(private bottomSheet: MatBottomSheet) {
  }

  openSideMenu() {
    this.bottomSheet.open(SideMenuComponent, {
      data: SideMenuLayouts.Full
    });
  }
}
