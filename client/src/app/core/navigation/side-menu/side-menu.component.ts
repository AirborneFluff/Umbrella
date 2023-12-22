import {Component, Input} from '@angular/core';
import { MENU_ENTRIES_FULL } from "../../components/menu-entries";
import { SideMenuLayouts } from "../side-menu-layouts";

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent {
  @Input() layoutType!: string;
  showDefaultAvatar = false;

  get widthClass(): string {
    if (this.layoutType == SideMenuLayouts.Full) return 'w-full';
    if (this.layoutType == SideMenuLayouts.Regular) return 'w-72';
    return 'w-24';
  }

  protected readonly entries = MENU_ENTRIES_FULL;
  protected readonly SideMenuLayouts = SideMenuLayouts;
}
