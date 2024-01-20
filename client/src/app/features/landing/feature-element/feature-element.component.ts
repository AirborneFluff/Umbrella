import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-feature-element',
  templateUrl: './feature-element.component.html',
  styleUrls: ['./feature-element.component.scss']
})
export class FeatureElementComponent {
  @Input() title!: string;
  @Input() icon!: string;
}
