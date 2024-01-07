import { Component } from '@angular/core';

@Component({
  selector: 'orb-search',
  templateUrl: './orb-search.component.html',
  styleUrls: ['./orb-search.component.scss']
})
export class OrbSearchComponent {
  protected _expanded = false;

  public expand() {
    this._expanded = true;
  }

  public collapse() {
    this._expanded = false;
  }
}
