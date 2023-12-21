import { Component } from '@angular/core';
import {MENU_ENTRIES_MOBILE} from "../../components/menu-entries";

@Component({
  selector: 'app-bottom-menu',
  templateUrl: './bottom-menu.component.html',
  styleUrls: ['./bottom-menu.component.scss']
})
export class BottomMenuComponent {
    protected readonly entries = MENU_ENTRIES_MOBILE;
}
