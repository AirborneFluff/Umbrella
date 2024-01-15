import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SideMenuLayouts } from "../side-menu-layouts";

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent {
  @Input() layoutType!: string;
  @Input() showCloseButton: boolean = false;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  showDefaultAvatar = false;

  get widthClass(): string {
    if (this.layoutType == SideMenuLayouts.Full) return 'w-full';
    if (this.layoutType == SideMenuLayouts.Regular) return 'w-72';
    return 'w-24';
  }

  get isCompact(): boolean {
    return this.layoutType == SideMenuLayouts.Compact;
  }

  protected readonly SideMenuLayouts = SideMenuLayouts;
}
