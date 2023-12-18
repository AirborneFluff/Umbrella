import { Component, Input } from '@angular/core';
import { MenuEntry } from '../menu-entry';

@Component({
  selector: 'menu-entry',
  templateUrl: './menu-entry.component.html',
  styleUrls: ['./menu-entry.component.scss']
})
export class MenuEntryComponent {
  @Input() entry!: MenuEntry;
}
