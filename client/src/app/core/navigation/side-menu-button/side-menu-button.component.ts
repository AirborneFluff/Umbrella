import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-side-menu-button',
  templateUrl: './side-menu-button.component.html',
  styleUrls: ['./side-menu-button.component.scss']
})
export class SideMenuButtonComponent {
  @Input() disabled: boolean = false;
  @Input() active: boolean = false;
  @Input() compact!: boolean;
  @Input() icon!: string;
  @Input() text!: string;

  get enabled() { return !this.disabled }
}
