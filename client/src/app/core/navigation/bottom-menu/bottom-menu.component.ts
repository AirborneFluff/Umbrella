import { Component } from '@angular/core';
import { RootFeatureStream } from '../../streams/root-feature-stream';
import { notNullOrUndefined } from '../../pipes/not-null';
import { RootFeatures } from '../../definitions/root-features';

@Component({
  selector: 'app-bottom-menu',
  templateUrl: './bottom-menu.component.html',
  styleUrls: ['./bottom-menu.component.scss']
})
export class BottomMenuComponent {

  constructor(private root$: RootFeatureStream) {}

  protected activeRoot$ = this.root$.pipe(notNullOrUndefined())

  protected readonly RootFeatures = RootFeatures;
}
