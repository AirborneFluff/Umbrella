import {Component, Input} from '@angular/core';

@Component({
  selector: 'bottom-menu-button',
  templateUrl: './bottom-menu-button.component.html',
  styleUrls: ['./bottom-menu-button.component.scss']
})
export class BottomMenuButtonComponent {
  @Input() active: boolean = false;
  @Input() icon!: string;
  @Input() text!: string;

}
