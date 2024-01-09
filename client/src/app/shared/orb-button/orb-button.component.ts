import {Component, Input} from '@angular/core';

@Component({
  selector: 'orb-button',
  templateUrl: './orb-button.component.html',
  styleUrls: ['./orb-button.component.scss']
})
export class OrbButtonComponent {
  @Input() icon!: string;
}
